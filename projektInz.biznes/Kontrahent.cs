using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektInz.dane
{
    public class Kontrahent
    {
        public int Id { get; set; }

        private string imię;

        public string Imię { get { return imię; } set { imię = value;} }

        public string Nazwisko { get; set; }
    }
}
