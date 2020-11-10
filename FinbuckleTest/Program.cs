using Finbuckle.MultiTenant;
using FinbuckleTest.Data;
using FinbuckleTest.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace FinbuckleTest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Set up the databases for the sample if needed.
            var env = host.Services.GetService<IWebHostEnvironment>();
            if (env.EnvironmentName == "Development")
            {
                var appSettingsJson = AppSettingsJson.GetAppSettings();
                var con = appSettingsJson["Finbuckle:MultiTenant:Stores:ConfigurationStore:Defaults:ConnectionString"];
                using (var db = new ApplicationDbContext(new TenantInfo { ConnectionString = con }))
                {
                    await db.Database.MigrateAsync();
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
