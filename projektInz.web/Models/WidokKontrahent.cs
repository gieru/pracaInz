using System.ComponentModel;
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

    public class NowyKontrahent
    {
        [MinLength(3)]
        [Required]
        public string Imię { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        public string NazwaFirmy { get; set; }
        [Required]
        public string Nip { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string NrTel { get; set; }
        public string Email { get; set; }
    }

    public class EdytowanyKontrahent
    {
        public int Id { get; set; }
        [MinLength(3)]
        [Required]
        public string Imię { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        public string NazwaFirmy { get; set; }
        [Required]
        public string Nip { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string NrTel { get; set; }
        public string Email { get; set; }

    }
}