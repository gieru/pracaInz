using System.Data.Entity.ModelConfiguration;
using projektInz.biznes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektInz.dane
{
    public class KontekstDanych : DbContext
    {
        public KontekstDanych()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KontekstDanych>());
        }

        public DbSet<Użytkownik> Użytkownicy { get; set; }
        public DbSet<Kontrahent> Kontrahenci { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Użytkownik>()
                .HasKey(x => x.Id)
                .ToTable("Uzytkownicy");

            var kontrahenci = modelBuilder.Entity<Kontrahent>();
            kontrahenci.HasKey(x => x.Id);
            kontrahenci.Property(x => x.Imię).HasColumnName("Imie").HasMaxLength(200).IsRequired().IsUnicode();
            kontrahenci.Property(x => x.Nazwisko).HasColumnName("Nazwisko").HasMaxLength(200).IsRequired().IsUnicode();
            kontrahenci.ToTable("Kontrahenci");
        }
    }
}
