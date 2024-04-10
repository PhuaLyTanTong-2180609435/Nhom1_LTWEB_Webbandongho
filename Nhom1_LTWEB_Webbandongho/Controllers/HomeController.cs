using Microsoft.AspNetCore.Mvc;
using Nhom1_LTWEB_Webbandongho.Models;
using X.PagedList;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Nhom1_LTWEB_Webbandongho.Repositories;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Nhom1_LTWEB_Webbandongho.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, IProductRepository
            productRepository, ICategoryRepository categoryRepository, ApplicationDbContext context)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }
        //lấy danh sách tất cả các sản phẩm trong DB
        public async Task<IActionResult> Index(int? page, String searchString)
        {
            if (page == null)
            {
                page = 1;
            }
            var pageNum = page ?? 1; 
            var pageSize = 4;
            var products = await _productRepository.GetAllAsync();
            if(!string.IsNullOrEmpty(searchString))
            {
               products = await _productRepository.GetByNameAsync(searchString);
            }
            var view = products.ToPagedList(pageNum, pageSize);
            return View(view);
        }
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        /* public JsonResult Autocomplete(string term)
         {

             var data = _productRepository.GetByNameAsync(term);

             return Json(data);
         }*/
        [HttpGet]
        public async Task<IActionResult> Autocomplete(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return new BadRequestObjectResult(new { message = "Invalid search term." });
            }

            string searchTerm = searchString.ToLower();

            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.ToLower().Contains(searchTerm))
                .Select(p => p.Name ) 
                .ToListAsync();

            return Json(products);
        }
        [Authorize]
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
