﻿using System;
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
        private EdytowaneZamowienie UtworzEdytowaneZamowienie(Zamowienie zamowienie, IEnumerable<Produkt> produkty)
        {
            return new EdytowaneZamowienie
            {
                Stan = zamowienie.Stan.ToString(),
                MoznaAnulowac = zamowienie.MoznaAnulowac && User.IsInRole("sprzedawca"),
                MoznaEdytowac = zamowienie.MoznaEdytowac && User.IsInRole("sprzedawca"),
                MoznaOplacic = zamowienie.MoznaOplacic && User.IsInRole("kasjer"),
                MoznaZatwierdzic = zamowienie.MoznaZatwierdzic && User.IsInRole("sprzedawca"),
                MoznaZrealizowac = zamowienie.MoznaZrealizowac && User.IsInRole("magazynier"),
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

        [Authorize(Roles = "sprzedawca")]
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

        [Authorize(Roles = "sprzedawca, kasjer, magazynier")]
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
        [Authorize(Roles = "sprzedawca")]
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

        [HttpPost]
        [Authorize(Roles = "sprzedawca")]
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
                    ModelState.AddModelError("Ilosc[" + zaktualizowanaPozycja.Numer + "]", "Ilość musi być poprawną liczbą.");
                    return View("EdytujZamowienie", UtworzEdytowaneZamowienie(zamowienie, dane.Produkty));
                }

                zamowienie.AktualizujPozycje(zaktualizowanaPozycja.Numer, ilosc);

                dane.SaveChanges();
                return RedirectToAction("EdytujZamowienie", new { id = zamowienie.Id });
            }
        }

        [HttpPost]
        [Authorize(Roles = "sprzedawca")]
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

        [HttpPost]
        [Authorize(Roles = "sprzedawca")]
        public ActionResult Zatwierdz(int id)
        {
            using (var dane = new KontekstDanych())
            {
                var zamowienie = dane.Zamowienia.First(x => x.Id == id);
                zamowienie.Zatwierdz();
                dane.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "kasjer")]
        public ActionResult Oplacono(int id)
        {
            using (var dane = new KontekstDanych())
            {
                var zamowienie = dane.Zamowienia.First(x => x.Id == id);
                zamowienie.Oplacono();
                dane.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "magazynier")]
        public ActionResult Zrealizowano(int id)
        {
            using (var dane = new KontekstDanych())
            {
                var zamowienie = dane.Zamowienia.First(x => x.Id == id);
                zamowienie.Zrealizowano();
                dane.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [Authorize(Roles = "sprzedawca")]
        public ActionResult Anuluj(int id)
        {
            using (var dane = new KontekstDanych())
            {
                //Pobranie
                var zamowienie = dane.Zamowienia.First(x => x.Id == id);
                zamowienie.Anuluj();
                dane.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}