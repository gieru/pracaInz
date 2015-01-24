using System;
using System.Collections.Generic;
using System.Linq;

namespace projektInz.biznes
{
    public class Zamowienie
    {
        public int Id { get; set; }
        public StanZamowienia Stan { get; set; }
        public virtual ICollection<Pozycja> Pozycje { get; set; }

        public decimal Wartosc
        {
            get { return Pozycje.Sum(x => x.Cena); }
        }

        public int DodajPozycje(Produkt produkt, int ilosc)
        {
            var numerNowejPozycji = NumerNowejPozycji();
            var pozycja = new Pozycja(this, numerNowejPozycji, produkt, ilosc);
            Pozycje.Add(pozycja);
            return numerNowejPozycji;
        }

        private int NumerNowejPozycji()
        {
            return Pozycje.Count + 1;
        }

        public void AktualizujPozycje(int numer, int nowaIlosc)
        {
            var pozycja = Pozycje.First(x => x.Numer == numer);
            pozycja.Aktulizuj(nowaIlosc);
        }

        public Pozycja UsunPozycje(int numer)
        {
            var usunietaPozycja = Pozycje.First(x => x.Numer == numer);
            Pozycje.Remove(usunietaPozycja);
            return usunietaPozycja;
        }

        public void Anuluj()
        {
            SprawdzStan(MoznaAnulowac);
            Stan = StanZamowienia.Anulowane;
        }

        public bool MoznaAnulowac
        {
            get { return Stan == StanZamowienia.Nowe; }
        }

        public void Zatwierdz()
        {
            SprawdzStan(MoznaZatwierdzic);
            Stan = StanZamowienia.DoZaplacenia;
        }

        public bool MoznaZatwierdzic
        {
            get { return Stan == StanZamowienia.Nowe; }
        }

        public bool MoznaEdytowac
        {
            get { return Stan == StanZamowienia.Nowe; }
        }
      
        public void Oplacono()
        {
            SprawdzStan(MoznaOplacic);
            Stan = StanZamowienia.Oplacone;
        }

        public bool MoznaOplacic
        {
            get { return Stan == StanZamowienia.DoZaplacenia; }
        }

        public void Zrealizowano()
        {
            SprawdzStan(MoznaZrealizowac);
            Stan = StanZamowienia.Zrealizowano;
        }

        public bool MoznaZrealizowac
        {
            get { return Stan == StanZamowienia.Oplacone; }
        }

        private void SprawdzStan(bool mozna)
        {
            if (!mozna)
            {
                throw new InvalidOperationException("Nie można anulować zamówienia w stanie " + Stan);
            }
        }

        public Zamowienie()
        {
            Pozycje = new List<Pozycja>();
        }
    }
}