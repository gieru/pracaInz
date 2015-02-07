using System;
using System.Collections.Generic;
using System.Linq;

namespace projektInz.biznes
{
    public partial class Zamowienie
    {
        public int Id { get; set; }
        public StanZamowienia Stan { get; set; }
        public virtual ICollection<PozycjaZamowienia> Pozycje { get; set; }

        public decimal WartoscBrutto
        {
            get { return Pozycje.Sum(x => x.CenaBrutto); }
        }
        
        public decimal WartoscNetto
        {
            get { return Pozycje.Sum(x => x.CenaNetto); }
        }

        public int DodajPozycje(Produkt produkt, int ilosc)
        {
            var numerNowejPozycji = NumerNowejPozycji();
            var pozycja = new PozycjaZamowienia(this, numerNowejPozycji, produkt, ilosc);
            Pozycje.Add(pozycja);
            return numerNowejPozycji;
        }

        public bool MoznaDodacPozycje(Produkt produkt, int ilosc)
        {
            return produkt.Stan - ilosc >= 0;
        }

        private int NumerNowejPozycji()
        {
            return Pozycje.Count + 1;
        }

        public bool MoznaAktualizowacPozycje(int numer, int nowaIlosc)
        {
            var pozycja = Pozycje.First(x => x.Numer == numer);
            return pozycja.MoznaAktualizowac(nowaIlosc);
        }

        public void AktualizujPozycje(int numer, int nowaIlosc)
        {
            var pozycja = Pozycje.First(x => x.Numer == numer);
            pozycja.Aktulizuj(nowaIlosc);
        }

        public PozycjaZamowienia UsunPozycje(int numer)
        {
            var usunietaPozycja = Pozycje.First(x => x.Numer == numer);
            Pozycje.Remove(usunietaPozycja);
            usunietaPozycja.ZwolnijBlokadeTowaru();
            return usunietaPozycja;
        }

        public void Anuluj()
        {
            SprawdzStan(MoznaAnulowac);
            Stan = StanZamowienia.Anulowane;
            foreach (var pozycja in Pozycje)
            {
                pozycja.ZwolnijBlokadeTowaru();
            }
        }

        public bool MoznaAnulowac
        {
            get { return Stan == StanZamowienia.Nowe; }
        }

        public Faktura Zatwierdz(GeneratorNumerowFaktur generatorNumerowFaktur)
        {
            SprawdzStan(MoznaZatwierdzic);
            Stan = StanZamowienia.DoZaplacenia;
            return new Faktura(this, generatorNumerowFaktur.GenerujNumer(DateTime.Now));
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
            Pozycje = new List<PozycjaZamowienia>();
        }
    }
}