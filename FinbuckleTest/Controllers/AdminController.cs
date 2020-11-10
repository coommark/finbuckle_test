using Finbuckle.MultiTenant;
using FinbuckleTest.Infrastructure;
using FinbuckleTest.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FinbuckleTest.Controllers
{
    public class AdminController : Controller
    {
        private IMultiTenantStore<ApplicationTenantInfo> _store { get; }
        private IServiceProvider _serviceProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager,
            IMultiTenantStore<ApplicationTenantInfo> store, IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _store = store;
            _serviceProvider = serviceProvider;
        }

        public ActionResult Create()
        {
            var tenants = _store.GetAllAsync().Result;
            ViewBag.TenantId = new SelectList(tenants, "Id", "Identifier");
            var model = new UserRegistrationModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRegistrationModel model)
        {
            //Correctly gets the current tenant
            var currentTenant = HttpContext.GetMultiTenantContext<ApplicationTenantInfo>()?.TenantInfo;

            //Correctly retrieves the target tenant
            var targetTenant = _store.TryGetAsync(model.TenantId.ToString()).Result;

            //Correctly sets the new tenant to the target tenant
            bool isSet = HttpContext.TrySetTenantInfo(targetTenant, true);

            //Refresh service?            
            //var userManager = HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
            var scope = _serviceProvider.CreateScope();
            var newContext = HttpContext.RequestServices = scope.ServiceProvider;
            var userManager = newContext.GetService<UserManager<ApplicationUser>>();

            //Verify that target tenant is correctly set, works
            var newTenant = HttpContext.GetMultiTenantContext<ApplicationTenantInfo>()?.TenantInfo;

            //Another Sample???
            //var accessor = _serviceProvider.GetRequiredService<IMultiTenantContextAccessor>();
            //var multiTenantContext = new MultiTenantContext<ApplicationTenantInfo>();
            //multiTenantContext.TenantInfo = newTenant;
            //accessor.MultiTenantContext = multiTenantContext;

            if (ModelState.IsValid)
            {
                var password = Guid.NewGuid().ToString("n").Substring(0, 8);
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name, PhoneNumber = model.PhoneNumber };

                //User is not created using the target tenant
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    //Send email with password, tenant name, login link
                }

                bool reset = HttpContext.TrySetTenantInfo(currentTenant, true);
            }

            var tenants = _store.GetAllAsync().Result;
            ViewBag.TenantId = new SelectList(tenants, "Id", "Identifier");
            return View(model);
        }
    }
}
