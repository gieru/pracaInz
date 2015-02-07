namespace projektInz.biznes
{
    public class Klienci
    {
        public int id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public string NazwaFirmy { get; set; }
        public string Nip { get; set; }
        public string Adres { get; set; }
        public string NrTel { get; set; }
        public string Email { get; set; }

        public Klienci()
        {
        }
        //Dodanie
        public Klienci(string imie, string nazwisko, string PESEL, string firma, string nip, string adres,
            string tel, string email)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = PESEL;
            NazwaFirmy = firma;
            Nip = nip;
            Adres = adres;
            NrTel = tel;
            Email = email;
        }
        //Edycja
        public void ZmienKlienta(string imie, string nazwisko, string PESEL, string firma, string nip, string adres,
            string tel, string email)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = PESEL;
            NazwaFirmy = firma;
            Nip = nip;
            Adres = adres;
            NrTel = tel;
            Email = email;
        }
    }
}
