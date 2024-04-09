
using Nhom1_LTWEB_Webbandongho.Models;

namespace Nhom1_LTWEB_Webbandongho.Repositories
{

    public interface IUserRespository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string id);
        Task AddAsync(ApplicationUser user);
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(string id);

    }
}
