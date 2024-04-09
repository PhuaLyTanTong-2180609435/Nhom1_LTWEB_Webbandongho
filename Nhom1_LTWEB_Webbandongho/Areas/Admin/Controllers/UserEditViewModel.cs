using System.Collections.Generic;
using Nhom1_LTWEB_Webbandongho.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Nhom1_LTWEB_Webbandongho.Areas.Admin.Controllers
{
    public class UserEditViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public string SelectedRole { get; set; }

    }
}
