using System.ComponentModel.DataAnnotations;

namespace projektInz.web.Models
{
    public class WidokKontrahent
    {
        public int Id { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string NazwaFirmy { get; set; }
        public string Nip { get; set; }
        public string Adres { get; set; }
        public string NrTel { get; set; }
        public string Email { get; set; }
    }
}