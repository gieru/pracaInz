using System;

namespace projektInz.biznes
{
    public class Produkt
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public DateTime DataWprowadzenia { get; set; }

        public Produkt()
        {
        }

        public Produkt(string nazwa)
        {
            DataWprowadzenia = DateTime.Now;
            Nazwa = nazwa;
        }

        public void ZmieńNazwę(string nowaNazwa)
        {
            Nazwa = nowaNazwa;
        }
    }
}