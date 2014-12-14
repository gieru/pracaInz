using System.Collections.Generic;

namespace projektInz.web.Models
{
    public class NowyUzytkownik
    {
        public string Login { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
    }

    public class IstniejacyUzytkownik
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Role { get; set; }
    }

    public class WidokUżytkownika
    {
        public string[] Role { get; set; } 
        public LocalPasswordModel Hasło { get; set; }

        public WidokUżytkownika()
        {
            Hasło = new LocalPasswordModel();
        }
    }
}