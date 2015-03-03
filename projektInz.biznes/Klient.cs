using System;

namespace projektInz.biznes
{
    public class Klient
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public string NazwaFirmy { get; set; }
        public string Nip { get; set; }
        public string Adres { get; set; }
        public string NrTel { get; set; }
        public string Email { get; set; }
        public TypKlienta Typ { get; set; }

        public string Nazwa
        {
            get
            {
                return Typ == TypKlienta.Firma 
                    ? NazwaFirmy 
                    : string.Format("{0} {1}", Imie, Nazwisko);
            }
        }

        public string Identyfikator
        {
            get
            {
                return Typ == TypKlienta.Firma 
                    ? Nip 
                    : Pesel;
            }
        }

        public Zamowienie UtworzZamowienie()
        {
            return new Zamowienie(this);
        }

        public Klient()
        {
        }
        //Dodanie
        public Klient(TypKlienta typKlienta, string imie, string nazwisko, string PESEL, string firma, string nip, string adres,
            string tel, string email)
        {
            Typ = typKlienta;
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
        public void ModyfikujDaneOsobyPrywatnej(string imie, string nazwisko, string adres, string tel, string email)
        {
            if (Typ != TypKlienta.OsobaPrywatna)
            {
                throw new InvalidOperationException("Oczekiwano osoby prywatnej");
            }
            Imie = imie;
            Nazwisko = nazwisko;
            Adres = adres;
            NrTel = tel;
            Email = email;
        }
        
        public void ModyfikujDaneFirmy(string firma, string adres, string tel, string email)
        {
            if (Typ != TypKlienta.Firma)
            {
                throw new InvalidOperationException("Oczekiwano firmy");
            }
            NazwaFirmy = firma;
            Adres = adres;
            NrTel = tel;
            Email = email;
        }
    }
}
