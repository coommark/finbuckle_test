
using Finbuckle.MultiTenant;
using FinbuckleTest.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FinbuckleTest.Data
{
    public class SharedDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private IConfiguration Configuration;

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // To prep each database uncomment the corresponding line below.
            var appSettingsJson = AppSettingsJson.GetAppSettings();
            var con = appSettingsJson["Finbuckle:MultiTenant:Stores:ConfigurationStore:Defaults:ConnectionString"];
            var tenantInfo = new TenantInfo { ConnectionString = con };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            return new ApplicationDbContext(tenantInfo, optionsBuilder.Options);
        }
    }
}
