using Microsoft.AspNetCore.Identity;
using Nhom1_LTWEB_Webbandongho.Models;
using System.Collections.Generic;


namespace Nhom1_LTWEB_Webbandongho.Areas.Admin.Controllers
{
    public class UserRolesViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public Dictionary<string, List<string>> UserRoles { get; set; }
    }
}
