using System.ComponentModel.DataAnnotations;

namespace projektInz.web.Models
{
    public class WidokProduktu
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string DataWprowadzenia { get; set; }
        public string Grupa { get; set; }
        public decimal Stan { get; set; }
        public decimal CenaZakupu { get; set; }
        public decimal CenaSprzedazy { get; set; }
        
    }

    public class NowyProdukt
    {
        [MinLength(3)]
        [Required]
        public string Nazwa { get; set; }
        public string Grupa { get; set; }
        public decimal Stan { get; set; }
        public decimal CenaZakupu { get; set; }
        public decimal CenaSprzedazy { get; set; }
    }

    public class EdytowanyProdukt
    {
        public int Id { get; set; }
        [MinLength(3)]
        [Required]
        public string Nazwa { get; set; }
        public string Grupa { get; set; }
        public decimal Stan { get; set; }
        public decimal CenaZakupu { get; set; }
        public decimal CenaSprzedazy { get; set; }
    }
}