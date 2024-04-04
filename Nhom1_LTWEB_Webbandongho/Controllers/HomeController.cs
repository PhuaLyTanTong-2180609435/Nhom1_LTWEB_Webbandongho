using Microsoft.AspNetCore.Mvc;
using Nhom1_LTWEB_Webbandongho.Models;
using X.PagedList;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Nhom1_LTWEB_Webbandongho.Repositories;

namespace Nhom1_LTWEB_Webbandongho.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(int ?page)
        {
            if (page == null)
            {
                page = 1;
            }
            var product = await _productRepository.GetAllAsync();
            int pageSize = 3;
            int pageNum = page ?? 1;
            return View(product.ToPagedList(pageNum,pageSize));
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
