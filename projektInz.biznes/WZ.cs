namespace projektInz.biznes
{
    public partial class WZ
    {
        public int Id { get; set; }
        public virtual Faktura Faktura { get; protected set; }
        public string Numer
        {
            get { return "WZ/" + Id; }
        }

        public WZ(Faktura faktura)
        {
            Faktura = faktura;
        }

        protected WZ()
        {
        }
    }
}