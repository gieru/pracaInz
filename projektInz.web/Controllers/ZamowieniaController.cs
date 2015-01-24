using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web.Mvc;
using projektInz.biznes;
using projektInz.dane;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    public class ZamowieniaController : Controller
    {
        public ActionResult Index()
        {
            List<Zamowienie> wszystkieZamowienia;
            //Otworz polaczenie z baza danych
            using (var dane = new KontekstDanych())
            {
                //Pobranie
                wszystkieZamowienia = dane.Zamowienia.ToList();
                var widoki = wszystkieZamowienia.Select(UtworzWidokZamowienia).ToList();
                //Wyrenderuj widok na bazie modelu widoki

                return View(widoki);
            }
        }

        private static WidokZamowienia UtworzWidokZamowienia(Zamowienie zamowienie)
        {
            return new WidokZamowienia()
            {
                Id = zamowienie.Id,
                Numer = zamowienie.Id.ToString(),
                IloscPozycji = zamowienie.Pozycje.Count,
                Wartosc = zamowienie.Wartosc
            };
        }
        private static EdytowaneZamowienie UtworzEdytowaneZamowienie(Zamowienie zamowienie, IEnumerable<Produkt> produkty)
        {
            return new EdytowaneZamowienie
            {
                Produkty = produkty.Select(x => new SelectListItem()
                {
                    Text = x.Nazwa,
                    Value = x.Id.ToString()
                }).ToList(),
                Id = zamowienie.Id,
                Numer = zamowienie.Id.ToString(),
                Pozycje = zamowienie.Pozycje.Select(pozycja => new WidokPozycjiZamowienia()
                {
                    Produkt = pozycja.Produkt.Nazwa,
                    CenaJednostkowa = pozycja.Produkt.CenaSprzedazy,
                    Ilosc = pozycja.Ilosc,
                    Cena = pozycja.Cena,
                    Numer = pozycja.Numer
                }).ToList()
            };
        }

        public ActionResult DodajZamowienie()
        {
            Zamowienie zamowienie;
            using (var dane = new KontekstDanych())
            {
                //Pobranie
                zamowienie = new Zamowienie();
                dane.Zamowienia.Add(zamowienie);
                dane.SaveChanges();
            }
            return RedirectToAction("EdytujZamowienie", new { id = zamowienie.Id });
        }

        public ActionResult EdytujZamowienie(int id)
        {
            using (var dane = new KontekstDanych())
            {
                //Pobranie
                var zamowienie = dane.Zamowienia.First(x => x.Id == id);
                var widok = UtworzEdytowaneZamowienie(zamowienie, dane.Produkty);
                return View(widok);
            }
        }

        [HttpPost]
        public ActionResult Anuluj(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult DodajPozycje(NowaPozycja nowaPozycja)
        {

            using (var dane = new KontekstDanych())
            {
                var zamowienie = dane.Zamowienia.First(x => x.Id == nowaPozycja.Id);

                if (!ModelState.IsValid)
                {
                    return View("EdytujZamowienie", UtworzEdytowaneZamowienie(zamowienie, dane.Produkty));
                }

                var produkt = dane.Produkty.First(x => x.Id == nowaPozycja.IdProduktu);
                zamowienie.DodajPozycje(produkt, nowaPozycja.Ilosc);

                dane.SaveChanges();
                return RedirectToAction("EdytujZamowienie", new { id = zamowienie.Id });
            }
        }

        public ActionResult AktualizujPozycje(ZaktualizowanaPozycja zaktualizowanaPozycja)
        {
            
            using (var dane = new KontekstDanych())
            {
                var zamowienie = dane.Zamowienia.First(x => x.Id == zaktualizowanaPozycja.Id);
                if (!ModelState.IsValid)
                {
                    return View("EdytujZamowienie", UtworzEdytowaneZamowienie(zamowienie, dane.Produkty));
                }

                int ilosc;
                if (!int.TryParse(zaktualizowanaPozycja.Ilosc[zaktualizowanaPozycja.Numer.ToString()], out ilosc))
                {
                    ModelState.AddModelError("Ilosc["+zaktualizowanaPozycja.Numer+"]", "Ilość musi być poprawną liczbą.");
                    return View("EdytujZamowienie", UtworzEdytowaneZamowienie(zamowienie, dane.Produkty));
                }

                zamowienie.AktualizujPozycje(zaktualizowanaPozycja.Numer, ilosc);

                dane.SaveChanges();
                return RedirectToAction("EdytujZamowienie", new { id = zamowienie.Id });
            }
        }

        public ActionResult UsunPozycje(UsunietaPozycja usunietaPozycja)
        {
            using (var dane = new KontekstDanych())
            {
                var zamowienie = dane.Zamowienia.First(x => x.Id == usunietaPozycja.Id);
                if (!ModelState.IsValid)
                {
                    return View("EdytujZamowienie", UtworzEdytowaneZamowienie(zamowienie, dane.Produkty));
                }
                var usunieta = zamowienie.UsunPozycje(usunietaPozycja.Numer);
                dane.Pozycje.Remove(usunieta);
                dane.SaveChanges();
                return RedirectToAction("EdytujZamowienie", new { id = zamowienie.Id });
            }
        }
    }
}