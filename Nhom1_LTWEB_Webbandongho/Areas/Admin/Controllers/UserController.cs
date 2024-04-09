using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Nhom1_LTWEB_Webbandongho.Areas.Admin.Models;
using Nhom1_LTWEB_Webbandongho.Models;
using Nhom1_LTWEB_Webbandongho.Repositories;
using X.PagedList;

namespace Nhom1_LTWEB_Webbandongho.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUserRespository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(IUserRespository userRepository, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1; // Số trang mặc định là trang 1
            var pageSize = 6; // Số lượng mục trên mỗi trang

            var users = await _userRepository.GetAllAsync();

            var userRoles = new Dictionary<string, List<string>>();
            foreach (var user in users)
            {
                var userRoleNames = await _userRepository.GetUserRolesAsync(user.Id);
                userRoles.Add(user.Id, userRoleNames.ToList());
            }

            var viewModel = new UserRolesViewModel
            {
                Users = users.ToPagedList(pageNumber, pageSize),
                UserRoles = userRoles
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Display(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            // Tìm người dùng với userId cung cấp
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Lấy tất cả các role hiện có từ RoleManager
            var allRoles = _roleManager.Roles.ToList();

            // Lấy role của người dùng
            var userRoles = await _userManager.GetRolesAsync(user);

            // Tạo danh sách SelectListItem từ danh sách role
            var roleItems = allRoles.Select(role => new SelectListItem
            {
                Text = role.Name,
                Value = role.Name,
                Selected = userRoles.Contains(role.Name)
            }).ToList();

            // Chuyển dữ liệu vào view model
            var viewModel = new UserEditViewModel
            {
                UserId = id,
                UserEmail = user.Email,
                Roles = roleItems
            };

            return View(viewModel);
        }

        // Action xử lý việc thay đổi role của người dùng
        [HttpPost]
        public async Task<IActionResult> UpdateRole(UserEditViewModel model)
        {
            // Tìm người dùng với userId cung cấp
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Xóa tất cả các role hiện tại của người dùng
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Thêm role mới đã chọn
            await _userManager.AddToRoleAsync(user, model.SelectedRole);

            return RedirectToAction(nameof(Index)); // Chuyển hướng sau khi thực hiện thay đổi
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

          
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
               
                return View(user);
            }

            var user_ = await _userRepository.GetByIdAsync(id);
            if (user_ == null)
            {
                return NotFound();
            }

            user_.PhoneNumber = user.PhoneNumber;           
            user_.FullName = user.FullName;
            user_.Age = user.Age;

          
            await _userRepository.UpdateAsync(user_);
            return RedirectToAction(nameof(Index));
        }


    }
}
