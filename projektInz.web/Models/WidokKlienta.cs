using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projektInz.web.Models
{
    public class WidokKlienta
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
    }
    public class NowyKlient
    {
        [MinLength(3)]
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        public string Pesel { get; set; }
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
    public class EdytowanyKlient
    {
        public int Id { get; set; }
        [MinLength(3)]
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }
        [Required]
        public string Pesel { get; set; }
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