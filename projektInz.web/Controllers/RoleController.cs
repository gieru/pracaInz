using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using projektInz.biznes;
using projektInz.dane;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Role/
        [HttpGet]
        public ActionResult EdytujRole(int id)
        {
            Użytkownik użytkownik;
            using (var dane = new KontekstDanych())
            {
                użytkownik = dane.Użytkownicy.Single(x => x.Id == id);
            }
            var role = Roles.GetRolesForUser(User.Identity.Name);
            var wszystkieRole = Roles.GetAllRoles();

            return View(new WidokRólUżytkownika()
            {
                NazwaWyświetlana = użytkownik.Nazwa,
                Role = wszystkieRole.ToDictionary(x => x, role.Contains)
            });
        }

        [HttpPost]
        public ActionResult EdytujRole(NoweRole model)
        {
            var noweRole = model.Role.Where(x => x.Value).Select(x => x.Key).ToArray();
            var stareRole = Roles.GetRolesForUser(User.Identity.Name);
            var wszystkieRole = Roles.GetAllRoles();

            var roleDoUsuniecia = stareRole.Except(noweRole).ToArray();
            var roleDoDodania = noweRole.Except(stareRole).ToArray();

            if (roleDoUsuniecia.Any())
            {
                Roles.RemoveUsersFromRoles(new[] {model.Login}, roleDoUsuniecia);
            }
            if (roleDoDodania.Any())
            {
                Roles.AddUsersToRoles(new[] {model.Login}, roleDoDodania);
            }

            return View(new WidokRólUżytkownika()
            {
                NazwaWyświetlana = model.Login,
                Role = wszystkieRole.ToDictionary(x => x, noweRole.Contains)
            });
        }

    }
}
