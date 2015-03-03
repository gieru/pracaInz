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

        public virtual Klient Klient { get; protected set; }
        public string NazwaKlienta { get; protected set; }
        public string NumerIdentyfikacyjnyKlienta { get; protected set; }
        public string AdresKlienta { get; protected set; }

        public decimal WartoscBrutto
        {
            get { return Pozycje.Sum(x => x.CenaBrutto); }
        }

        public decimal Podatek
        {
            get { return WartoscBrutto - WartoscNetto; }
        }

        public decimal WartoscNetto
        {
            get { return Pozycje.Sum(x => x.CenaNetto); }
        }

        public Faktura(Zamowienie zamowienie, string numer)
        {
            Zamowienie = zamowienie;
            Numer = numer;
            DataWystawienia = DateTime.Now;
            Pozycje = zamowienie.Pozycje.Select(poz => new PozycjaFaktury(this, poz.Produkt, poz.Ilosc)).ToList();
            NazwaKlienta = zamowienie.Klient.Nazwa;
            NumerIdentyfikacyjnyKlienta = zamowienie.Klient.Identyfikator;
            AdresKlienta = zamowienie.Klient.Adres;
            Klient = zamowienie.Klient;
        }

        protected Faktura()
        {
            Pozycje = new List<PozycjaFaktury>();
        }
    }
}