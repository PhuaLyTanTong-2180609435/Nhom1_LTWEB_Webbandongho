using Microsoft.AspNetCore.Identity;
using Nhom1_LTWEB_Webbandongho.Models;
using System.Collections.Generic;
using X.PagedList;

namespace Nhom1_LTWEB_Webbandongho.Areas.Admin.Controllers
{
    public class UserRolesViewModel
    {
        public IPagedList<ApplicationUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public Dictionary<string, List<string>> UserRoles { get; set; }
       
    }
}
