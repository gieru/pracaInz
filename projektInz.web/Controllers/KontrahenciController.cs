using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using projektInz.biznes;
using projektInz.dane;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    [Authorize(Roles = "admin")]
    public class KontrahenciController : Controller
    {
        public ActionResult Index()
        {
            List<Kontrahent> allKontrahent;
            using (var dane = new KontekstDanych())
            {
                allKontrahent = dane.Kontrahenci.ToList();
            }

            var widoki = allKontrahent.Select(kontrahent => new WidokKontrahent
            {
                Id = kontrahent.Id,
                Imię = kontrahent.Imię,
                Nazwisko = kontrahent.Nazwisko,
                NazwaFirmy = kontrahent.NazwaFirmy,
                Nip = kontrahent.Nip,
                Adres = kontrahent.Adres,
                NrTel = kontrahent.NrTel,
                Email = kontrahent.Email
            }).ToList();

            return View(widoki);
        }

        public ActionResult DodajKontrahenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DodajKontrahenta(NowyKontrahent nowyKontrahent)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var dane = new KontekstDanych())
            {
                var kontrahent = new Kontrahent(
                    nowyKontrahent.Imię,
                    nowyKontrahent.Nazwisko,
                    nowyKontrahent.NazwaFirmy,
                    nowyKontrahent.Nip,
                    nowyKontrahent.Adres,
                    nowyKontrahent.NrTel,
                    nowyKontrahent.Email
                    );
                dane.Kontrahenci.Add(kontrahent);
                dane.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult EdytujKontrahent(int id)
        {
            Kontrahent kontrahent;
            using (var dane = new KontekstDanych())
            {
                kontrahent = dane.Kontrahenci.Single(x => x.Id == id);
            }
            return View(new WidokKontrahent
            {
                Imię = kontrahent.Imię,
                Nazwisko = kontrahent.Nazwisko,
                NazwaFirmy = kontrahent.NazwaFirmy,
                Nip = kontrahent.Nip,
                Adres = kontrahent.Adres,
                NrTel = kontrahent.NrTel,
                Email = kontrahent.Email
            });
        }

        [HttpPost]
        public ActionResult EdytujKontrahent(EdytowanyKontrahent edytowanyKontrahent)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var dane = new KontekstDanych())
            {
                var kontrahent = dane.Kontrahenci.Single(x => x.Id == edytowanyKontrahent.Id);

                kontrahent.ZmienKontrahent(edytowanyKontrahent.Imię,
                    edytowanyKontrahent.Nazwisko,
                    edytowanyKontrahent.NazwaFirmy,
                    edytowanyKontrahent.Adres,
                    edytowanyKontrahent.Nip,
                    edytowanyKontrahent.NrTel,
                    edytowanyKontrahent.Email);
                dane.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
