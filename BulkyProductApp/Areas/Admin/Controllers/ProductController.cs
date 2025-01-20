using Bulky.DataAccess.Repositories.IRepository;
using Bulky.Models.Models;
using Bulky.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulky.Models.ViewModels;
using Bulky.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace BulkyProductApp.Areas.Admin.Product
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            //get Products data from the database and pass it to the Index View
            //var objProductsList = _dbContext.Products.ToList();
            //or
        
            List<Products> objProductsList = _UnitOfWork.Products.GetAll(includeProperties: "Category").ToList();
            return View(objProductsList);
        }

        public IActionResult Upsert(int? id)
        {
            
            IEnumerable<SelectListItem> CategoryList = _UnitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
            ViewBag.CategoryList = CategoryList;

            ProductViewModel ProductVM = new()
            {
                CategoryList = CategoryList,
                Products = new Products()
            };

            if (id == null || id == 0) { 
                //Create
                return View(ProductVM);
            }
            else
            {
                //Update
                ProductVM.Products = _UnitOfWork.Products.GetFirstOrDefault(u=> u.ProductId ==  id);
                return View(ProductVM); 
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\Product");

                    if(!string.IsNullOrEmpty(obj.Products.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Products.ImageUrl.TrimStart('/'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Products.ImageUrl = $"/Images/Product/{fileName}";
                }
                if(obj.Products.ProductId == 0)
                {

                _UnitOfWork.Products.Add(obj.Products);
                }
                else
                {
                    _UnitOfWork.Products.Update(obj.Products);
                }
                _UnitOfWork.Save();
                TempData["success"] = "Products Operation Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<SelectListItem> CategoryList = _UnitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
                ViewBag.CategoryList = CategoryList;

                ProductViewModel ProductVM = new()
                {
                    CategoryList = CategoryList,
                    Products = new Products()
                };
                return View(ProductVM);
            }
        }

       

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Method 1
            //Find by default will check for primary key
            Products? product = _UnitOfWork.Products.GetFirstOrDefault(u => u.ProductId == id);
            // Method 2
            //fisrt or default is able to check for other fields than primary key
            //Products? product = _dbContext.Products.FirstOrDefault(u => u.ProductsId == id);
            //Method 3
            //we can use Where Clause for further condition to fetch the data
            //Products? product = _dbContext.Products.Where(u => u.ProductsId == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            IEnumerable<SelectListItem> CategoryList = _UnitOfWork.Category.GetAll().Select(
               u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.CategoryId.ToString()
               });
            //ViewData["CategoryList"] = CategoryList;
            ProductViewModel ProductVM = new()
            {
                CategoryList = CategoryList,
                Products= new Products()
            };

            return View(ProductVM);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Products? obj = _UnitOfWork.Products.GetFirstOrDefault(u => u.ProductId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Products.Delete(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Products Deleted Successfully";
            return RedirectToAction("Index");

        }

        #region ApiCalls
        [HttpGet]
        public IActionResult GetAll() {
            List<Products> objProductsList = _UnitOfWork.Products.GetAll(includeProperties: "Category").ToList();
            return Json(new
            {
                data = objProductsList,
            });
        }
        #endregion
    }
}
