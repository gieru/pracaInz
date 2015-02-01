using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using projektInz.biznes;
using projektInz.dane;
using projektInz.web.Models;

namespace projektInz.web.Controllers
{
    public class CustomersController : Controller
    {
        //
        // GET: /Customers/

        public ActionResult Index()
        {
            List<Customers> allCustomers;
            using (var dane = new KontekstDanych())
            {
                allCustomers = dane.Customers.ToList();
            }

            var widoki = allCustomers.Select(customers => new WidokKlienta
            {
                Id = customers.id,
                Imię = customers.Imie,
                Nazwisko = customers.Nazwisko,
                NazwaFirmy = customers.NazwaFirmy,
                Nip = customers.Nip,
                Adres = customers.Adres,
                NrTel = customers.NrTel,
                Email = customers.Email
            }).ToList();

            return View(widoki);
        }

    }
}
