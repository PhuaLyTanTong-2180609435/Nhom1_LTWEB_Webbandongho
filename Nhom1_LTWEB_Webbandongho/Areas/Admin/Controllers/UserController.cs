using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(IUserRespository userRespository, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userRespository = userRespository;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var users = await _userManager.Users.ToListAsync();

            // Lấy danh sách tất cả vai trò
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = new Dictionary<string, List<string>>();

            foreach (var user in users)
            {
                var userRoleNames = await _userManager.GetRolesAsync(user);
                userRoles.Add(user.Id, userRoleNames.ToList());
            }

            var viewModel = new UserRolesViewModel { Users = users, Roles = roles, UserRoles = userRoles };
            return View(viewModel);
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

        public async Task<IActionResult> Update(string id, string roleName, bool isAddRole)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }

            if (isAddRole)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }

            return RedirectToAction(nameof(Index));
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationUser user, string roleName, bool isAddRole)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                // Cập nhật thông tin người dùng
                existingUser.FullName = user.FullName;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Age = user.Age;

                var result = await _userManager.UpdateAsync(existingUser);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(user);
                }

                // Kiểm tra và cập nhật vai trò của người dùng
                if (!string.IsNullOrEmpty(roleName))
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role == null)
                    {
                        return NotFound();
                    }

                    if (isAddRole)
                    {
                        await _userManager.AddToRoleAsync(existingUser, roleName);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(existingUser, roleName);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }
    }
    }
