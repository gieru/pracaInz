using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projektInz.biznes;
using projektInz.dane;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    [Authorize(Roles = "magazynier, sprzedawca, admin")]
    public class ZamowieniaProduktuController : Controller
    {
        [Authorize(Roles = "magazynier, sprzedawca, admin")]
        [HttpGet]
        public ActionResult Index()
        {
            using (var dane = new KontekstDanych())
            {
                var model = dane.ZamowieniaProduktow.ToList().Select(x => new WidokZamowieniaProduktu()
                {
                    Produkt = x.Produkt.Nazwa,
                    JM = x.JM,
                    DataZlozenia = x.DataZlozenia.ToShortDateString(),
                    Ilosc = x.Ilosc
                }).ToList();
                return View(model);
            }
        }

        [Authorize(Roles = "magazynier, admin")]
        [HttpGet]
        public ActionResult Zamow(int id)
        {
            Produkt produkt;
            using (var dane = new KontekstDanych())
            {
                produkt = dane.Produkty.Single(x => x.Id == id);
            }
            return View(new WidokZamawianegoProduktu()
            {
                IdProduktu = id,
                Produkt = produkt.Nazwa,
                JM = produkt.JM
            });
        }

        [Authorize(Roles = "magazynier, admin")]
        [HttpPost]
        public ActionResult Zamow(ZamowProdukt zamowProdukt)
        {
            if (zamowProdukt.Ilosc <= 0)
            {
                ModelState.AddModelError("Ilosc", "Ilość musi być dodatnia.");
            }
            if (!ModelState.IsValid)
            {
                Produkt produkt;
                using (var dane = new KontekstDanych())
                {
                    produkt = dane.Produkty.Single(x => x.Id == zamowProdukt.IdProduktu);
                }
                return View(new WidokZamawianegoProduktu()
                {
                    IdProduktu = zamowProdukt.IdProduktu,
                    Produkt = produkt.Nazwa,
                    JM = produkt.JM
                });
            }
            using (var dane = new KontekstDanych())
            {
                var produkt = dane.Produkty.Single(x => x.Id == zamowProdukt.IdProduktu);
                var zamowienie = produkt.Zamow(zamowProdukt.Ilosc, zamowProdukt.JM);
                dane.ZamowieniaProduktow.Add(zamowienie);
                dane.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
