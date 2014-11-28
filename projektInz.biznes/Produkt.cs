using System;

namespace projektInz.biznes
{
    public class Produkt
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Grupa { get; set; }
        public decimal Stan { get; set; }
        public decimal CenaZakupu { get; set; }
        public decimal CenaSprzedazy { get; set; }
        public DateTime DataWprowadzenia { get; set; }


        public Produkt()
        {
        }
        //dodaj produkt
        public Produkt(string nazwa, string grupa, decimal stan, decimal cenaZakupu, decimal cenaSprzedazy)
        {
            DataWprowadzenia = DateTime.Now;
            Nazwa = nazwa;
            Grupa = grupa;
            Stan = stan;
            CenaZakupu = cenaZakupu;
            CenaSprzedazy = cenaSprzedazy;
        }

        //edytacja produktu
        public void zmienProdukt(string nowaNazwa, string nowaGrupa, decimal nowyStan, decimal nowaCenaZakupu,
            decimal nowaCenaSprzedazy)
        {
            DataWprowadzenia = DateTime.Now;
            Nazwa = nowaNazwa;
            Grupa = nowaGrupa;
            Stan = nowyStan;
            CenaZakupu = nowaCenaZakupu;
            CenaSprzedazy = nowaCenaSprzedazy;
        }
    }
}