using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinbuckleTest.Models.Account
{
    [MultiTenant]
    public class ApplicationRole : IdentityRole<int>
    {
        public string Access { get; set; }

        [DisplayName("Created")]
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public int UsersCount { get; set; }
    }
}
