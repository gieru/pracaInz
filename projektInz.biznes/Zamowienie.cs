using System.Collections.Generic;
using System.Linq;

namespace projektInz.biznes
{
    public class Zamowienie
    {
        public int Id { get; set; }
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

        public Zamowienie()
        {
            Pozycje = new List<Pozycja>();
        }
    }
}