using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using projektInz.biznes;
using projektInz.dane;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    [Authorize(Roles = "admin, sprzedawca, kasjer")]
    public class KlienciController : Controller
    {
        //
        // GET: /Customers/

        public ActionResult Index()
        {
            List<Klienci> wszyscyKlienci;
            using (var dane = new KontekstDanych())
            {
                wszyscyKlienci = dane.Klienci.ToList();
            }

            var widoki = wszyscyKlienci.Select(klient => new WidokKlienta
            {
                Id = klient.id,
                Imie = klient.Imie,
                Nazwisko = klient.Nazwisko,
                NazwaFirmy = klient.NazwaFirmy,
                Nip = klient.Nip,
                Adres = klient.Adres,
                NrTel = klient.NrTel,
                Email = klient.Email
            }).ToList();

            return View(widoki);
        }
        public ActionResult DodajKlienta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DodajKlienta(NowyKlient nowyKlient)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var dane = new KontekstDanych())
            {
                var klient = new Klienci(
                    nowyKlient.Imie,
                    nowyKlient.Nazwisko,
                    nowyKlient.Pesel,
                    nowyKlient.NazwaFirmy,
                    nowyKlient.Nip,
                    nowyKlient.Adres,
                    nowyKlient.NrTel,
                    nowyKlient.Email
                    );
                dane.Klienci.Add(klient);
                dane.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult EdytujKlienta(int id)
        {
            Klienci klient;
            using (var dane = new KontekstDanych())
            {
                klient = dane.Klienci.Single(x => x.id == id);
            }
            return View(new WidokKlienta
            {
                Imie = klient.Imie,
                Nazwisko = klient.Nazwisko,
                Pesel = klient.Pesel,
                NazwaFirmy = klient.NazwaFirmy,
                Nip = klient.Nip,
                Adres = klient.Adres,
                NrTel = klient.NrTel,
                Email = klient.Email
            });
        }

        [HttpPost]
        public ActionResult EdytujKlienta(EdytowanyKlient edytowanyKlient)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var dane = new KontekstDanych())
            {
                var klient = dane.Klienci.Single(x => x.id == edytowanyKlient.Id);

                klient.ZmienKlienta(edytowanyKlient.Imie,
                    edytowanyKlient.Nazwisko,
                    edytowanyKlient.Pesel,
                    edytowanyKlient.NazwaFirmy,
                    edytowanyKlient.Adres,
                    edytowanyKlient.Nip,
                    edytowanyKlient.NrTel,
                    edytowanyKlient.Email);
                dane.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
