using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
using System.Web.UI.WebControls;
using projektInz.web.Controllers;

namespace projektInz.web.Helpers
{
    public static class MenuHelper
    {
        private static Type[] kontrolery =
        {
            typeof(ProduktyController),
            typeof(KontrahenciController),
            typeof(UzytkownicyController),
            typeof(ZamowieniaController),
        };

   
        public static IEnumerable<HtmlString> Menu (this HtmlHelper helper)
        {
            foreach (var kontroler in kontrolery)
            {
                //Budujemy nazwe linku na podstawie nazwy klasy kontrolera KontrahenciController -> Kontrahenci
                var linkName = kontroler.Name.Replace("Controller", "");
                //Sprawdzamy czy klasa kontrolera ma atrybut [Authorize]
                var authorize = kontroler.GetCustomAttribute<AuthorizeAttribute>(false);
                if (authorize == null)
                {
                    //Jesli nie to wszyscy maja do niej dostep
                    yield return helper.ActionLink(linkName, "Index", linkName);
                }
                else
                {
                    //zmieniamy string z oddzielonymi przecinkami rolami na tablie stringow np "a, b" -> [a,b]
                    var role =
                        authorize.Roles.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => x.Trim(' '))
                            .ToArray();
                    //Jesli ktorakolwiek (Any) rola jest przypisana zalogowanemu uzytkownikowi to generujemy link
                    if (role.Any(Roles.IsUserInRole))
                    {
                        yield return helper.ActionLink(linkName, "Index", linkName);
                    }
                }
            }
        } 
    }
}