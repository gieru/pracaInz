using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using projektInz.biznes;
using projektInz.dane;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    [Authorize(Roles = "admin")]
    public class KontrahenciController : Controller
    {
        public ActionResult Index()
        {
            List<Kontrahent> allKontrahent;
            using (var dane = new KontekstDanych())
            {
                allKontrahent = dane.Kontrahenci.ToList();
            }

            var widoki = allKontrahent.Select(kontrahent => new WidokKontrahent
            {
                Id = kontrahent.Id,
                Imię = kontrahent.Imię,
                Nazwisko =kontrahent.Nazwisko,
                NazwaFirmy = kontrahent.NazwaFirmy,
                Nip = kontrahent.Nip,
                Adres = kontrahent.Adres,
                NrTel = kontrahent.NrTel,
                Email = kontrahent.Email
            }).ToList();

            return View(widoki);
        }

        public ActionResult DodajKontrahenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DodajKontrahenta(NowyKontrahent nowyKontrahent)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var dane = new KontekstDanych())
            {
                var kontrahent = new Kontrahent(
                    nowyKontrahent.Imię,
                    nowyKontrahent.Nazwisko,
                    nowyKontrahent.NazwaFirmy,
                    nowyKontrahent.Nip,
                    nowyKontrahent.Adres,
                    nowyKontrahent.NrTel,
                    nowyKontrahent.Email
                    );
                dane.Kontrahenci.Add(kontrahent);
                dane.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}