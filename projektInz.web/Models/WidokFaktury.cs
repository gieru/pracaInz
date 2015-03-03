using System.Collections.Generic;

namespace projektInz.web.Models
{
    public class FakturyKlienta
    {
        public string Nazwa { get; set; }
        public string Identyfikator { get; set; }
        public List<WidokFaktury> Faktury { get; set; }
    }

    public class WidokFaktury
    {
        public int Id { get; set; }
        public string Numer { get; set; }
        public int IloscPozycji { get; set; }
        public decimal WartoscNetto { get; set; }
        public decimal WartoscBrutto { get; set; }
    }

    public class WidokWydrukuFaktury
    {
        public string Numer { get; set; }
        public string NazwaKlienta { get; set; }
        public string IdentyfikatorKlienta { get; set; }
        public string AdresKlienta { get; set; }
        public decimal Podatek { get; set; }
        public decimal WartoscBrutto { get; set; }
        public decimal WartoscNetto { get; set; }
        public List<WidokPozycjiFaktury> Pozycje { get; set; }
    }

    public class WidokPozycjiFaktury
    {
        public decimal Ilosc { get; set; }
        public string Produkt { get; set; }
        public decimal CenaJednostkowaNetto { get; set; }
        public decimal CenaBrutto { get; set; }
        public decimal CenaNetto { get; set; }
        public string JM { get; set; }
    }
}