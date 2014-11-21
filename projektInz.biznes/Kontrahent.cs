namespace projektInz.biznes
{
    public class Kontrahent
    {
        public int Id { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string NazwaFirmy { get; set; }
        public string Nip { get; set; }
        public string Adres { get; set; }
        public string NrTel { get; set; }
        public string Email { get; set; }

        public Kontrahent()
        {
        }

        public Kontrahent(string imię, string nazwisko, string firma, string nip, string adres,
            string tel, string email)
        {
            Imię = imię;
            Nazwisko = nazwisko;
            NazwaFirmy = firma;
            Nip = nip;
            Adres = adres;
            NrTel = tel;
            Email = email;
        }


    }
}
