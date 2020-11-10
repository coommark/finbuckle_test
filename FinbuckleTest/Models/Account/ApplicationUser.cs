using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinbuckleTest.Models.Account
{
    [MultiTenant]
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }

        [Display(Name = "Profile Image")]
        public string ProfileImage { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        [Display(Name = "Date Created")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Last Login")]
        public DateTime LastLoginDate { get; set; } = DateTime.UtcNow;

        public string TenantId { get; set; }
    }
}
