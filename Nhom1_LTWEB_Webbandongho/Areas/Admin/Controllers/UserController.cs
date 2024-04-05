using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly ApplicationDbContext _context;

        public UserController(IUserRespository userRespository, ApplicationDbContext context)
        {
            _userRespository = userRespository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userRespository.GetAllAsync();
            
            _context.UserRoles
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
    }
}
