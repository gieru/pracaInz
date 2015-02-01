namespace projektInz.biznes
{
    public class Customers
    {
        public int id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string pesel { get; set; }
        public string NazwaFirmy { get; set; }
        public string Nip { get; set; }
        public string Adres { get; set; }
        public string NrTel { get; set; }
        public string Email { get; set; }
    }
    public Customers(){}

    public Customers(string imię, string nazwisko,string PESEL, string firma, string nip, string adres,
            string tel, string email){
    imie = Imie;
    nazwisko = Nazwisko;
    pesel = PESEL;
    firma = NazwaFirmy;
    nip = Nip;
    adres = Adres;
    tel = NrTel;
    email = Email;
    }
}
