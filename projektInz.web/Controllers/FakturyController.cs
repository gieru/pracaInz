using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using projektInz.biznes;
using projektInz.dane;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    [Authorize(Roles = "admin, sprzedawca, kasjer")]
    public class FakturyController : Controller
    {
        public ActionResult Index(int id)
        {
            //Otworz polaczenie z baza danych
            using (var dane = new KontekstDanych())
            {
                //Pobranie
                var klient = dane.Klienci.First(x => x.Id == id);
                var wszystkieFaktury = dane.Faktury.Where(x => x.Klient.Id == id).ToList();
                var faktury = wszystkieFaktury.Select(UtworzWidokFaktury).ToList();
                //Wyrenderuj widok na bazie modelu widoki

                return View(new FakturyKlienta
                {
                    Nazwa = klient.Nazwa,
                    Identyfikator = klient.Identyfikator,
                    Faktury = faktury
                });
            }
        }

        public ActionResult Drukuj(int id)
        {
            using (var dane = new KontekstDanych())
            {
                var faktura = dane.Faktury.First(x => x.Id == id);
                return View(KonwertujDoWidoku(faktura));
            }
        }
        
        public ActionResult DrukujWZ(int id)
        {
            using (var dane = new KontekstDanych())
            {
                var wz = dane.WZ.First(x => x.Id == id);
                ViewBag.NumerWZ = wz.Numer;
                return View(KonwertujDoWidoku(wz.Faktura));
            }
        }

        private static WidokWydrukuFaktury KonwertujDoWidoku(Faktura faktura)
        {
            return new WidokWydrukuFaktury()
            {
                AdresKlienta = faktura.AdresKlienta,
                IdentyfikatorKlienta = faktura.NumerIdentyfikacyjnyKlienta,
                NazwaKlienta = faktura.NazwaKlienta,
                Numer = faktura.Numer,
                Podatek = faktura.Podatek,
                WartoscBrutto = faktura.WartoscBrutto,
                WartoscNetto = faktura.WartoscNetto,
                Pozycje = faktura.Pozycje.Select(poz => new WidokPozycjiFaktury()
                {
                    Produkt = poz.Produkt.Nazwa,
                    CenaBrutto = poz.CenaBrutto,
                    CenaNetto = poz.CenaNetto,
                    CenaJednostkowaNetto = poz.Produkt.CenaSprzedazyNetto,
                    Ilosc = poz.Ilosc,
                    JM = poz.Produkt.JM
                }).ToList()
            };
        }

        private static WidokFaktury UtworzWidokFaktury(Faktura faktura)
        {
            return new WidokFaktury()
            {
                Id = faktura.Id,
                Numer = faktura.Numer,
                IloscPozycji = faktura.Pozycje.Count,
                WartoscBrutto = faktura.WartoscBrutto,
                WartoscNetto = faktura.WartoscNetto,
            };
        }
    }
}