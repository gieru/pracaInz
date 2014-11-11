namespace projektInz.biznes
{
    public class Kontrahent
    {
        public int Id { get; set; }

        public string Imię { get; set; }

        public string Nazwisko { get; set; }

        public Kontrahent()
        {
        }

        public Kontrahent(string imię, string nazwisko)
        {
            Imię = imię;
            Nazwisko = nazwisko;
        }


    }
}
