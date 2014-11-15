using System.Collections.Generic;
using System.Web.Mvc;
using projektInz.biznes;
using projektInz.dane;
using System.Linq;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    [Authorize(Roles = "Magazynier")]
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

            //var widoki = new List<WidokProduktu>();
            //foreach (var produkt in wszystkieProdukty)
            //{
            //    var widok = new WidokProduktu
            //    {
            //        Nazwa = produkt.Nazwa,
            //        DataWprowadzenia = produkt.DataWprowadzenia.ToShortDateString()

            //    };
            //    widoki.Add(widok);
            //}

            var widoki = wszystkieProdukty.Select(produkt => new WidokProduktu
            {
                Id = produkt.Id,
                Nazwa = produkt.Nazwa,
                DataWprowadzenia = produkt.DataWprowadzenia.ToShortDateString()

            }).ToList();
            //Wyrenderuj widok na bazie modelu widoki
            return View(widoki);
        }

        public ActionResult EdytujProdukt(int id)
        {
            Produkt produkt;
            using (var dane = new KontekstDanych())
            {
                produkt = dane.Produkty.Single(x => x.Id == id);
            }
            return View(new WidokProduktu
            {
                Id = produkt.Id,
                Nazwa = produkt.Nazwa,
                DataWprowadzenia = produkt.DataWprowadzenia.ToShortDateString()
            });
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
                produkt.ZmieńNazwę(edytowanyProdukt.Nazwa);
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
                var produkt = new Produkt(nowyProdukt.Nazwa);
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