using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using projektInz.web.Models;
using projektInz.dane;

namespace projektInz.web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                try
                {
                    new KontekstDanych().Database.Initialize(true);
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Uzytkownicy", "Id", "Login", autoCreateTables: true);

                    var roleDomyslne = new[] {"admin","sprzedawca","magazynier","kasjer"};
                    var roleIstniejace = Roles.GetAllRoles();

                    foreach (var brakujacaRola in roleDomyslne.Except(roleIstniejace))
                    {
                        Roles.CreateRole(brakujacaRola);
                        
                    }
                    if (!Roles.IsUserInRole("admin", "admin"))
                    {
                        Roles.AddUserToRole("admin", "admin");
                    }
                    if (WebSecurity.GetCreateDate("admin") == DateTime.MinValue)
                    {
                        WebSecurity.CreateAccount("admin", "admin1");
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
