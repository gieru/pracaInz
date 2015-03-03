using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using projektInz.biznes;
using projektInz.dane;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    [Authorize(Roles = "admin, magazynier")]
    public class WZController : Controller
    {
        public ActionResult Index()
        {
            //Otworz polaczenie z baza danych
            using (var dane = new KontekstDanych())
            {
                //Pobranie
                var model = dane.WZ.ToList().Select(x => new WidokWZ()
                {
                    Id = x.Id,
                    Numer = x.Numer
                }).ToList();

                return View(model);
            }
        }
    }
}