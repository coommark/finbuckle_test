using Finbuckle.MultiTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinbuckleTest.Infrastructure
{
    public class ApplicationTenantInfo : ITenantInfo
    {
        public string Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ConnectionString { get; set; }
        public string CookiePath { get; set; }
        public string CookieLoginPath { get; set; }
        public string CookieLogoutPath { get; set; }
        public string CookieAccessDeniedPath { get; set; }
    }
}
