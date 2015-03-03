using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using projektInz.biznes;
using projektInz.dane;
using System.Linq;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProduktyController : Controller
    {
        public ActionResult Index()
        {
            List<Produkt> wszystkieProdukty;
            //Otworz polaczenie z baza danych
            using (var dane = new KontekstDanych())
            {
                //Pobranie
                wszystkieProdukty = dane.Produkty.ToList();
            }

            var widoki = wszystkieProdukty.Select(KonwertujProdukt).ToList();
            //Wyrenderuj widok na bazie modelu widoki
            return View(widoki);
        }

        private WidokProduktu KonwertujProdukt(Produkt produkt)
        {
            return new WidokProduktu
            {
                Id = produkt.Id,
                Nazwa = produkt.Nazwa,
                DataWprowadzenia = produkt.DataWprowadzenia.ToShortDateString(),
                CenaSprzedazyNetto = produkt.CenaSprzedazyNetto,
                CenaZakupuNetto = produkt.CenaZakupuNetto,
                Grupa = produkt.Grupa,
                JM = produkt.JM,
                Stan = produkt.Stan,
                StawkaVat = (int) (produkt.StawkaVat * 100),
                Marza = (int)(produkt.Marza * 100)
            };
        }

        public ActionResult EdytujProdukt(int id)
        {
            Produkt produkt;
            using (var dane = new KontekstDanych())
            {
                produkt = dane.Produkty.Single(x => x.Id == id);
            }
            return View(KonwertujProdukt(produkt));
        }

        [HttpPost]
        public ActionResult EdytujProdukt(EdytowanyProdukt edytowanyProdukt)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var dane = new KontekstDanych())
            {
                var produkt = dane.Produkty.Single(x => x.Id == edytowanyProdukt.Id);
                produkt.ZmienProdukt(edytowanyProdukt.Nazwa,
                    edytowanyProdukt.Grupa, 
                    edytowanyProdukt.JM, 
                    edytowanyProdukt.Stan, 
                    (decimal)edytowanyProdukt.StawkaVat / 100, 
                    edytowanyProdukt.CenaZakupuNetto,
                    (decimal)edytowanyProdukt.Marza / 100);

                //Wysyła zmiany do bazy
                dane.SaveChanges();
            }
            //Przenosi uzytkownika do innej akcji (index)
            return RedirectToAction("Index");
        }

        public ActionResult DodajProdukt()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DodajProdukt(NowyProdukt nowyProdukt)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var dane = new KontekstDanych())
            {
                var produkt = new Produkt(nowyProdukt.Nazwa,
                    nowyProdukt.Grupa,
                    nowyProdukt.JM,
                    nowyProdukt.Stan,
                    (decimal)nowyProdukt.StawkaVat / 100,
                    nowyProdukt.CenaZakupuNetto,
                    (decimal)nowyProdukt.Marza / 100);
                //Dodaje nowy produkt do unit of work, ale jeszcze nie do bazy danych
                dane.Produkty.Add(produkt);
                //Wysyła zmiany do bazy
                dane.SaveChanges();
            }
            //Przenosi uzytkownika do innej akcji (index)
            return RedirectToAction("Index");
        }
    }
}