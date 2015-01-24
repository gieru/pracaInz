namespace projektInz.biznes
{
    public class PozycjaFaktury
    {
        public int Id { get; protected set; }
        public virtual Faktura Faktura { get; protected set; }
        public virtual Produkt Produkt { get; protected set; }
        public int Ilosc { get; protected set; }

        public decimal Cena
        {
            get { return Ilosc * Produkt.CenaSprzedazy; }
        }

        public PozycjaFaktury(Faktura faktura, Produkt produkt, int ilosc)
        {
            Faktura = faktura;
            Produkt = produkt;
            Ilosc = ilosc;
        }
    }

}