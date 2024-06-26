﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nhom1_LTWEB_Webbandongho.Areas.Admin.Models
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public static class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Company = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
    }
}
