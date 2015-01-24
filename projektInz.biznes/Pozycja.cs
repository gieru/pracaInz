using System;

namespace projektInz.biznes
{
    public class Pozycja
    {
        //Entity framework potrzebuje ID
        public int Id { get; protected set; }
        public virtual Zamowienie Zamowienie { get; protected set; }

        public int Numer { get; protected set; }
        public virtual Produkt Produkt { get; protected set; }
        public int Ilosc { get; protected set; }

        public decimal Cena
        {
            get { return Ilosc*Produkt.CenaSprzedazy; }
        }

        public Pozycja(Zamowienie zamowienie, int numer, Produkt produkt, int ilosc)
        {
            Numer = numer;
            Produkt = produkt;
            Ilosc = ilosc;
            Zamowienie = zamowienie;
        }

        protected Pozycja()
        {
        }

        public void Aktulizuj(int nowaIlosc)
        {
            if (nowaIlosc < 0)
            {
                throw new InvalidOperationException("Ilość musi być większa od zera.");
            }
            Ilosc = nowaIlosc;
        }
    }
}