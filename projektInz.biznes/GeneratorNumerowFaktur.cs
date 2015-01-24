using System;

namespace projektInz.biznes
{
    public class GeneratorNumerowFaktur
    {
        public int Id { get; set; }
        public int AktualnyRok { get; set; }
        public int OstatnioWygenerowanyNumer { get; set; }

        public GeneratorNumerowFaktur(DateTime aktualnaData)
        {
            AktualnyRok = aktualnaData.Year;
        }

        public string GenerujNumer(DateTime aktualnaData)
        {
            if (AktualnyRok == aktualnaData.Year)
            {
                OstatnioWygenerowanyNumer++;
            }
            else
            {
                OstatnioWygenerowanyNumer = 1;
                AktualnyRok = aktualnaData.Year;
            }
            return string.Format("FV/{0}/{1}", OstatnioWygenerowanyNumer, AktualnyRok);
        }

        protected GeneratorNumerowFaktur()
        {
        }
    }
}