using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nhom1_LTWEB_Webbandongho.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Age { get; set; }
    }
}
