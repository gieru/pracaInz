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
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private string[] RoleWbudowane = new string[] { "admin" };
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
            var role = Roles.GetRolesForUser(użytkownik.Login);
            var wszystkieRole = Roles.GetAllRoles();

            return View(new WidokRólUżytkownika()
            {
                NazwaWyświetlana = użytkownik.Imię + " " + użytkownik.Nazwisko,
                Login = użytkownik.Login,
                Id = użytkownik.Id,
                Role = wszystkieRole.Select(x => new RolaUzytkownika
                {
                    CzyMaRole = role.Contains(x),
                    Nazwa = x,
                    CzyMoznaEdytowac = CzyMoznaEdytowac(role, x)
                }).ToList()
            });
        }

        private bool CzyMoznaEdytowac(string[] role, string x)
        {
            return !role.Contains(x) || !RoleWbudowane.Contains(x);
        }

        [HttpPost]
        public ActionResult EdytujRole(NoweRole model)
        {
            var roleModel = model.Role ?? new Dictionary<string, bool>();
            var noweRole = roleModel.Where(x => x.Value).Select(x => x.Key).ToArray();
            var stareRole = Roles.GetRolesForUser(model.Login);

            var roleDoUsuniecia = stareRole.Except(noweRole).Except(RoleWbudowane).ToArray();
            var roleDoDodania = noweRole.Except(stareRole).ToArray();

            if (roleDoUsuniecia.Any())
            {
                Roles.RemoveUsersFromRoles(new[] { model.Login }, roleDoUsuniecia);
            }
            if (roleDoDodania.Any())
            {
                Roles.AddUsersToRoles(new[] { model.Login }, roleDoDodania);
            }

            return RedirectToAction("Index", "Uzytkownicy", new {id = model.Id});
        }

    }
}
