using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using projektInz.biznes;
using projektInz.dane;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    [Authorize(Roles = "Administrator")]
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
    }
}