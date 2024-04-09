
using Nhom1_LTWEB_Webbandongho.Models;

namespace Nhom1_LTWEB_Webbandongho.Repositories
{

    public interface IUserRespository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string id);
        Task UpdateAsync(ApplicationUser user);
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);
        Task AddUserToRoleAsync(ApplicationUser user, string roleName);
        Task RemoveUserFromRoleAsync(ApplicationUser user, string roleName);
        Task<bool> IsUserInRoleAsync(ApplicationUser user, string roleName);
        Task EditUserRoleAsync(string userId, string roleName);
    }
}
