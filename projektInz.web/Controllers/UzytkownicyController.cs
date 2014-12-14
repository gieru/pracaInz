using projektInz.dane;
using projektInz.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projektInz.web.Controllers
{
    public class UzytkownicyController : Controller
    {
        //
        // GET: /Uzytkownicy/

        public ActionResult Index()
        {
            List<IstniejacyUzytkownik> uzytkownicy;
            using (var kontekst = new KontekstDanych())
            {
                uzytkownicy = kontekst.Użytkownicy.Select(x => new IstniejacyUzytkownik {
                    Login = x.Login
                }).ToList();
            }
            return View();
        }

        [HttpGet]
        public ActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dodaj(NowyUzytkownik nowyUzytkownik)
        {
            return View();
        }
    }
}
