using System.Diagnostics;
using Bulky.DataAccess.Repositories.IRepository;
using Bulky.Models;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyProductApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Products> productList = _unitOfWork.Products.GetAll(
                includeProperties:"Category"
                );
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            Products product = _unitOfWork.Products.GetFirstOrDefault(
                u => u.ProductId == id,includeProperties:"Category"
                );
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
