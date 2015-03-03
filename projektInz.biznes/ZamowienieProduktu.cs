using System;

namespace projektInz.biznes
{
    public class ZamowienieProduktu
    {
        public int Id { get; set; }
        public virtual Produkt Produkt { get; protected set; }
        public decimal Ilosc { get; protected set; }
        public string JM { get; protected set; }
        public DateTime DataZlozenia { get; protected set; }

        public ZamowienieProduktu(Produkt produkt, decimal ilosc, string jm)
        {
            Produkt = produkt;
            Ilosc = ilosc;
            JM = jm;
            DataZlozenia = DateTime.Now;
        }

        public ZamowienieProduktu()
        {
        }
    }
}