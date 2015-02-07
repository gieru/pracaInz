using System;

namespace projektInz.biznes
{
    public class PozycjaZamowienia
    {
        public int Id { get; protected set; }
        public virtual Zamowienie Zamowienie { get; protected set; }

        public int Numer { get; protected set; }
        public virtual Produkt Produkt { get; protected set; }
        public int Ilosc { get; protected set; }

        public decimal CenaBrutto
        {
            get { return Ilosc * Produkt.CenaSprzedazyBrutto; }
        }

        public decimal CenaNetto
        {
            get { return Ilosc * Produkt.CenaSprzedazyNetto; }
        }

        public PozycjaZamowienia(Zamowienie zamowienie, int numer, Produkt produkt, int ilosc)
        {
            Numer = numer;
            Produkt = produkt;
            Ilosc = ilosc;
            Zamowienie = zamowienie;
            Produkt.Stan -= ilosc;
        }

        protected PozycjaZamowienia()
        {
        }

        public void Aktulizuj(int nowaIlosc)
        {
            if (nowaIlosc < 0)
            {
                throw new InvalidOperationException("Ilość musi być większa od zera.");
            }
            var roznica = nowaIlosc - Ilosc;
            Produkt.Stan -= roznica;
            Ilosc = nowaIlosc;
        }

        public void ZwolnijBlokadeTowaru()
        {
            Produkt.Stan += Ilosc;
        }

        public bool MoznaAktualizowac(int nowaIlosc)
        {
            var roznica = nowaIlosc - Ilosc;
            return Produkt.Stan - roznica >= 0;
        }
    }
}