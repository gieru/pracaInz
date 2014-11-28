using System.Collections.Generic;

namespace projektInz.web.Models
{
    public class WidokRólUżytkownika
    {
        public string NazwaWyświetlana { get; set; }
        public Dictionary<string, bool> Role { get; set; } 
    }

    public class NoweRole
    {
        public string Login { get; set; }
        public Dictionary<string, bool> Role { get; set; } 
    }
}