using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nhom1_LTWEB_Webbandongho.Areas.Admin.Models;
using Nhom1_LTWEB_Webbandongho.Models;
using Nhom1_LTWEB_Webbandongho.Repositories;


namespace Nhom1_LTWEB_Webbandongho.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUserRespository _userRespository;
        
     

        public UserController(IUserRespository userRespository)
        {
            _userRespository = userRespository;
            
           
        }
        public async Task<IActionResult> Index()
        {

            var users = await _userRespository.GetAllAsync();            
            return View(users);
        }
        public async Task<IActionResult> Display(string id)
        {
            var users = await _userRespository.GetByIdAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        public async Task<IActionResult> Update(string id)
        {
            var users = await _userRespository.GetByIdAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            
            return View(users);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationUser user)
        {
           
            if (ModelState.IsValid)
            {
                var existingProduct = await _userRespository.GetByIdAsync(id);
                
                // Cập nhật các thông tin khác của sản phẩm
                existingProduct.FullName = user.FullName;
                existingProduct.PhoneNumber = user.PhoneNumber;
                existingProduct.Age = user.Age;
               
                await _userRespository.UpdateAsync(existingProduct);
                return RedirectToAction(nameof(Index));
            }
           
            return View(user);
        }
    }
}
