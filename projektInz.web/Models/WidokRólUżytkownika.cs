using System.Collections.Generic;

namespace projektInz.web.Models
{
    public class WidokRólUżytkownika
    {
        public string NazwaWyświetlana { get; set; }
        public string Login { get; set; }
        public int Id { get; set; }
        public List<RolaUzytkownika> Role { get; set; } 
    }

    public class RolaUzytkownika
    {
        public string Nazwa { get; set; }
        public bool CzyMaRole { get; set; }
        public bool CzyMoznaEdytowac { get; set; }
    }


    public class NoweRole
    {
        public string Login { get; set; }
        public Dictionary<string, bool> Role { get; set; }
        public int Id { get; set; }
    }
}