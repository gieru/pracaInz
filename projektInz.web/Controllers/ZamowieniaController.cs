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
    [Authorize(Roles = "admin, sprzedawca, kasjer")]
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
                Stan = zamowienie.Stan.ToString(),
                Numer = zamowienie.Id.ToString(),
                IloscPozycji = zamowienie.Pozycje.Count,
                WartoscBrutto = zamowienie.WartoscBrutto,
                WartoscNetto = zamowienie.WartoscNetto,
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
                    CenaJednostkowaNetto = pozycja.Produkt.CenaSprzedazyNetto,
                    Ilosc = pozycja.Ilosc,
                    CenaNetto = pozycja.CenaNetto,
                    CenaBrutto = pozycja.CenaBrutto,
                    StawkaVat = (int) (pozycja.Produkt.StawkaVat * 100),
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
                var produkt = dane.Produkty.First(x => x.Id == nowaPozycja.IdProduktu);

                if (!ModelState.IsValid)
                {
                    return View("EdytujZamowienie", UtworzEdytowaneZamowienie(zamowienie, dane.Produkty));
                }
                if (!zamowienie.MoznaDodacPozycje(produkt, nowaPozycja.Ilosc))
                {
                    ModelState.AddModelError("Ilosc", "Brak wymaganej ilości towaru na magazynie. Dostępne "+produkt.Stan);
                    return View("EdytujZamowienie", UtworzEdytowaneZamowienie(zamowienie, dane.Produkty));
                }
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
                if (!zamowienie.MoznaAktualizowacPozycje(zaktualizowanaPozycja.Numer, ilosc))
                {
                    var pozycja = zamowienie.Pozycje.First(x => x.Numer == zaktualizowanaPozycja.Numer);
                    ModelState.AddModelError("Ilosc[" + zaktualizowanaPozycja.Numer + "]", "Brak wymaganej ilości towaru na magazynie. Dostępne " + pozycja.Produkt.Stan);
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
                dane.PozycjeZamowien.Remove(usunieta);
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
                var generator = dane.GeneratoryNumerowFaktur.First();
                var faktura = zamowienie.Zatwierdz(generator);
                dane.Faktury.Add(faktura);
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