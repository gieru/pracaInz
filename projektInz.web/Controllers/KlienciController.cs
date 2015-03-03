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
            List<Klient> wszyscyKlienci;
            using (var dane = new KontekstDanych())
            {
                wszyscyKlienci = dane.Klienci.ToList();
            }

            var widoki = wszyscyKlienci.Select(klient => new WidokKlienta
            {
                Id = klient.Id,
                Nazwa = klient.Nazwa,
                PeselNip = klient.Identyfikator,
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
            if (nowyKlient.TypKlienta == TypKlienta.Firma)
            {
                if (string.IsNullOrWhiteSpace(nowyKlient.NazwaFirmy))
                {
                    ModelState.AddModelError("NazwaFirmy","Nazwa firmy jest wymagana.");
                }
                if (string.IsNullOrWhiteSpace(nowyKlient.Nip))
                {
                    ModelState.AddModelError("Nip", "Numer NIP jest wymagany.");
                }
            }
            else if (nowyKlient.TypKlienta == TypKlienta.OsobaPrywatna)
            {
                if (string.IsNullOrWhiteSpace(nowyKlient.Imie))
                {
                    ModelState.AddModelError("Imie", "Imie jest wymagane.");
                }
                if (string.IsNullOrWhiteSpace(nowyKlient.Nazwisko))
                {
                    ModelState.AddModelError("Nazwisko", "Nazwisko jest wymagane.");
                }
                if (string.IsNullOrWhiteSpace(nowyKlient.Pesel))
                {
                    ModelState.AddModelError("Pesel", "Pesel jest wymagany.");
                }
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            using (var dane = new KontekstDanych())
            {
                var klient = new Klient(
                    nowyKlient.TypKlienta,
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
            Klient klient;
            using (var dane = new KontekstDanych())
            {
                klient = dane.Klienci.Single(x => x.Id == id);
            }
            if (klient.Typ == TypKlienta.Firma)
            {
                return View("EdytujFirme", new WidokFirmy
                {
                    NazwaFirmy = klient.NazwaFirmy,
                    Nip = klient.Nip,
                    Adres = klient.Adres,
                    NrTel = klient.NrTel,
                    Email = klient.Email
                });
            }
            return View("EdytujOsobePrywatna", new WidokOsobyPrywatnej
            {
                Imie = klient.Imie,
                Nazwisko = klient.Nazwisko,
                Pesel = klient.Pesel,
                Adres = klient.Adres,
                NrTel = klient.NrTel,
                Email = klient.Email
            });
        }

        [HttpPost]
        public ActionResult EdytujOsobePrywatna(EdytowanaOsobaPrywatna osobaPrywatna)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var dane = new KontekstDanych())
            {
                var klient = dane.Klienci.Single(x => x.Id == osobaPrywatna.Id);

                klient.ModyfikujDaneOsobyPrywatnej(osobaPrywatna.Imie,
                    osobaPrywatna.Nazwisko,
                    osobaPrywatna.Adres,
                    osobaPrywatna.NrTel,
                    osobaPrywatna.Email);
                dane.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult EdytujFirme(EdytowanaFirma edytowanaFirma)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var dane = new KontekstDanych())
            {
                var klient = dane.Klienci.Single(x => x.Id == edytowanaFirma.Id);

                klient.ModyfikujDaneFirmy(
                    edytowanaFirma.NazwaFirmy,
                    edytowanaFirma.Adres,
                    edytowanaFirma.NrTel,
                    edytowanaFirma.Email);
                dane.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
