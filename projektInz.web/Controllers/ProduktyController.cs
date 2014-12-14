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
                Nazwa = produkt.Nazwa,
                Grupa = produkt.Grupa,
                Stan = produkt.Stan,
                CenaZakupu = produkt.CenaZakupu,
                CenaSprzedazy = produkt.CenaSprzedazy,
                Id = produkt.Id,
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
                produkt.ZmienProdukt(edytowanyProdukt.Nazwa,
                    edytowanyProdukt.Grupa, 
                    edytowanyProdukt.CenaSprzedazy, 
                    edytowanyProdukt.CenaZakupu, 
                    edytowanyProdukt.Stan);

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
                    nowyProdukt.CenaSprzedazy,
                    nowyProdukt.CenaZakupu,
                    nowyProdukt.Stan);
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