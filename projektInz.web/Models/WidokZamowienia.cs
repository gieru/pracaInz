﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace projektInz.web.Models
{
    public class WidokZamowienia
    {
        public int Id { get; set; }
        public string Stan { get; set; }
        public string Numer { get; set; }
        public int IloscPozycji { get; set; }
        public decimal Wartosc { get; set; }
    }

    public class NoweZamowienie
    {
        public List<NowaPozycja> Pozycje { get; set; }
    }

    public class EdytowaneZamowienie
    {
        public string Stan { get; set; }

        public bool MoznaEdytowac { get; set; }
        public bool MoznaAnulowac { get; set; }
        public bool MoznaZatwierdzic { get; set; }
        public bool MoznaOplacic { get; set; }
        public bool MoznaZrealizowac { get; set; }

        public List<SelectListItem> Produkty { get; set; }
        public int Id { get; set; }
        public string Numer { get; set; }
        public List<WidokPozycjiZamowienia> Pozycje { get; set; }
    }
    
    public class WidokPozycjiZamowienia
    {
        public int Numer { get; set; }
        public decimal Ilosc { get; set; }
        public string Produkt { get; set; }
        public decimal CenaJednostkowa { get; set; }
        public decimal Cena { get; set; }
    }

    public class NowaPozycja
    {
        public int Id { get; set; }
        public int Ilosc { get; set; }
        public int IdProduktu { get; set; }
    }

    public class ZaktualizowanaPozycja
    {
        public int Id { get; set; }
        public int Numer { get; set; }
        public Dictionary<string, string> Ilosc { get; set; }
    }

    public class UsunietaPozycja
    {
        public int Id { get; set; }
        public int Numer { get; set; }
    }
}