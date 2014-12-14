using System.Data.Entity;
using System.Linq;
using System.Web.Security;
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
            context.SaveChanges();
        }
    }
}