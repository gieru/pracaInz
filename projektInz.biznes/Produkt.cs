using System;

namespace projektInz.biznes
{
    public class Produkt
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Grupa { get; set; }
        public decimal Stan { get; set; }
        public decimal Marza { get; set; }
        public decimal CenaZakupuNetto { get; set; }
        public decimal StawkaVat { get; set; }

        public decimal VatNaliczony
        {
            get { return CenaZakupuNetto*StawkaVat; }
        }

        public decimal CenaZakupuBrutto
        {
            get { return CenaZakupuNetto + VatNaliczony; }
        }

        public decimal CenaSprzedazyNetto
        {
            get { return CenaZakupuNetto*(Marza + 1); }
        }

        public decimal VatNalezny
        {
            get { return CenaSprzedazyNetto * StawkaVat; }
        }

        public decimal CenaSprzedazyBrutto
        {
            get { return CenaSprzedazyNetto + VatNalezny; }
        }

        public DateTime DataWprowadzenia { get; set; }


        public Produkt()
        {
        }
        //dodaj produkt
        public Produkt(string nazwa, string grupa, decimal stan, decimal stawkaVat, decimal cenaZakupuNetto,
            decimal marza)
        {
            if (stawkaVat > 1)
            {
                throw new ArgumentException("Stawka VAT musi byc liczbą z zakresu [0,1]");
            }
            DataWprowadzenia = DateTime.Now;
            Nazwa = nazwa;
            Grupa = grupa;
            Stan = stan;
            StawkaVat = stawkaVat;
            CenaZakupuNetto = cenaZakupuNetto;
            Marza = marza;
        }

        //edytacja produktu
        public void ZmienProdukt(string nazwa, string grupa, decimal stan, decimal stawkaVat, decimal cenaZakupuNetto,
            decimal marza)
        {
            if (stawkaVat > 1)
            {
                throw new ArgumentException("Stawka VAT musi byc liczbą z zakresu [0,1]");
            }
            DataWprowadzenia = DateTime.Now;
            Nazwa = nazwa;
            Grupa = grupa;
            Stan = stan;
            StawkaVat = stawkaVat;
            CenaZakupuNetto = cenaZakupuNetto;
            Marza = marza;
        }
    }
}