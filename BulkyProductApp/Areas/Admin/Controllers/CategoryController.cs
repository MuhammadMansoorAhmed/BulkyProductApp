using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repositories.IRepository;
using Bulky.Models.Models;
using Bulky.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyProductApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =StaticDetails.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            _UnitOfWork = db;
        }
        public IActionResult Index()
        {
            //get Category data from the database and pass it to the Index View
            //var objCategoryList = _dbContext.Category.ToList();
            //or
            List<Category> objCategoryList = _UnitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        //this action is by default a get action method
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Method 1
            //Find by default will check for primary key
            Category? category = _UnitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            // Method 2
            //fisrt or default is able to check for other fields than primary key
            //Category? category = _dbContext.Category.FirstOrDefault(u => u.CategoryId == id);
            //Method 3
            //we can use Where Clause for further condition to fetch the data
            //Category? category = _dbContext.Category.Where(u => u.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Category.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Method 1
            //Find by default will check for primary key
            Category? category = _UnitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            // Method 2
            //fisrt or default is able to check for other fields than primary key
            //Category? category = _dbContext.Category.FirstOrDefault(u => u.CategoryId == id);
            //Method 3
            //we can use Where Clause for further condition to fetch the data
            //Category? category = _dbContext.Category.Where(u => u.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _UnitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Category.Delete(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

        }

    }
}
