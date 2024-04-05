using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nhom1_LTWEB_Webbandongho.Areas.Admin.Models;
using Nhom1_LTWEB_Webbandongho.Models;
using Nhom1_LTWEB_Webbandongho.Repositories;

namespace Nhom1_LTWEB_Webbandongho.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CatelogyController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CatelogyController(IProductRepository productRepository,
        ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var catelogys = await _categoryRepository.GetAllAsync();
            return View(catelogys);
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            await _categoryRepository.AddAsync(category);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var catelogy = await _categoryRepository.GetByIdAsync(id);
            if (catelogy == null)
            {
                return NotFound();
            }
           
            return View(catelogy);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id,Category category)
        {
           
            if (ModelState.IsValid)
            {
                var existingCatelogy = await _categoryRepository.GetByIdAsync(id);
               
                // Cập nhật các thông tin khác của sản phẩm
                existingCatelogy.Name = category.Name;
                
                await _categoryRepository.UpdateAsync(existingCatelogy);
                return RedirectToAction(nameof(Index));
            }
           
            return View(category);
        }
    }
}
