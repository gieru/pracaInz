﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektInz.biznes
{
    public class Użytkownik
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }

        public Użytkownik(string login, string imię, string nazwisko)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            if (imię == null)
            {
                throw new ArgumentNullException("imię");
            }
            if (nazwisko == null)
            {
                throw new ArgumentNullException("nazwisko");
            }
            Login = login;
            Imię = imię;
            Nazwisko = nazwisko;
        }

        public Użytkownik()
        {
        }
    }
}
