using System;
using System.Collections.Generic;
using System.Linq;

namespace projektInz.biznes
{
    public partial class Faktura
    {
        public int Id { get; set; }
        public Zamowienie Zamowienie { get; protected set; }
        public string Numer { get; protected set; }
        public DateTime DataWystawienia { get; protected set; }
        public virtual ICollection<PozycjaFaktury> Pozycje { get; protected set; } 

        public Faktura(Zamowienie zamowienie, string numer)
        {
            Zamowienie = zamowienie;
            Numer = numer;
            DataWystawienia = DateTime.Now;
            Pozycje = zamowienie.Pozycje.Select(poz => new PozycjaFaktury(this, poz.Produkt, poz.Ilosc)).ToList();
        }

        protected Faktura()
        {
            Pozycje = new List<PozycjaFaktury>();
        }
    }
}