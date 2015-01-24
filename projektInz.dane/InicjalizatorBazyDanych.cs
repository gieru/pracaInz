using System;
using System.Data.Entity;
using System.Linq;
using projektInz.biznes;

namespace projektInz.dane
{
    public class InicjalizatorBazyDanych : DropCreateDatabaseIfModelChanges<KontekstDanych>
    {
        public override void InitializeDatabase(KontekstDanych context)
        {
            base.InitializeDatabase(context);

            var adminIstnieje = context.U¿ytkownicy.Any(x => x.Login == "admin");
            if (!adminIstnieje)
            {
                var admin = new U¿ytkownik()
                {
                    Login = "admin",
                    Imiê = "admin",
                    Nazwisko = "admin"
                };
                context.U¿ytkownicy.Add(admin);
            }

            var generatorNumerowFaktur = context.GeneratoryNumerowFaktur.FirstOrDefault();
            if (generatorNumerowFaktur == null)
            {
                generatorNumerowFaktur = new GeneratorNumerowFaktur(DateTime.Now);
                context.GeneratoryNumerowFaktur.Add(generatorNumerowFaktur);
            }

            context.SaveChanges();
        }
    }
}