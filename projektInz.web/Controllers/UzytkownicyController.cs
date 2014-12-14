using System.Web.Security;
using projektInz.biznes;
using projektInz.dane;
using projektInz.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace projektInz.web.Controllers
{
    [Authorize(Roles = "admin")]
    public class UzytkownicyController : Controller
    {
        //
        // GET: /Uzytkownicy/

        public ActionResult Index()
        {
            List<IstniejacyUzytkownik> uzytkownicy;
            using (var kontekst = new KontekstDanych())
            {
                uzytkownicy = kontekst.Użytkownicy.ToList().Select(x => new IstniejacyUzytkownik {
                    Id = x.Id,
                    Login = x.Login,
                    Imie = x.Imię,
                    Nazwisko = x.Nazwisko,
                    Role = String.Join(", ", Roles.GetRolesForUser(x.Login))
                }).ToList();
            }
            return View(uzytkownicy);
        }

        [HttpGet]
        public ActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dodaj(NowyUzytkownik nowyUzytkownik)
        {
            if (ModelState.IsValid)
            {
                var uzytkownik = new Użytkownik(nowyUzytkownik.Login, nowyUzytkownik.Imie, nowyUzytkownik.Nazwisko);
                using (var kontekst = new KontekstDanych())
                {
                    var loginZajety = kontekst.Użytkownicy.Any(x => x.Login == nowyUzytkownik.Login);
                    if (loginZajety)
                    {
                        ModelState.AddModelError("Login","Podany login jest zajęty");
                        return View();
                    }
                    kontekst.Użytkownicy.Add(uzytkownik);
                    kontekst.SaveChanges();

                    WebSecurity.CreateAccount(uzytkownik.Login, "haslo");
                }
                return RedirectToAction("EdytujRole", "Role", new {id = uzytkownik.Id});
            }
            return View();
        }
    }
}
