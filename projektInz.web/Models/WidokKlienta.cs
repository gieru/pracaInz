using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projektInz.web.Models
{
    public class WidokKlienta
    {       
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string pesel { get; set; }
        public string NazwaFirmy { get; set; }
        public string Nip { get; set; }
        public string Adres { get; set; }
        public string NrTel { get; set; }
        public string Email { get; set; }
    }
}