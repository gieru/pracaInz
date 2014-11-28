using System.Collections.Generic;

namespace projektInz.web.Models
{
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