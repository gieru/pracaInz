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
        public DbSet<Produkt> Produkty { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Użytkownik>()
                .HasKey(x => x.Id)
                .ToTable("Uzytkownicy");

            var kontrahenci = modelBuilder.Entity<Kontrahent>();
            kontrahenci.HasKey(x => x.Id);
            kontrahenci.Property(x => x.Imię).HasColumnName("Imie").HasMaxLength(100).IsRequired().IsUnicode();
            kontrahenci.Property(x => x.Nazwisko).HasColumnName("Nazwisko").HasMaxLength(100).IsRequired().IsUnicode();
            kontrahenci.Property(x => x.NazwaFirmy)
                .HasColumnName("Nazwa Firmy")
                .HasMaxLength(200)
                .IsRequired()
                .IsUnicode();
            kontrahenci.Property(x => x.Nip).HasMaxLength(13).IsRequired();
            kontrahenci.Property(x => x.Adres).HasMaxLength(200).IsRequired();
            kontrahenci.Property(x => x.NrTel).HasMaxLength(20).IsRequired();
            kontrahenci.Property(x => x.Email).HasMaxLength(50).IsRequired();
            kontrahenci.ToTable("Kontrahenci");

            var produkty = modelBuilder.Entity<Produkt>();
            produkty.HasKey(x => x.Id);
            produkty.Property(x => x.Nazwa).HasMaxLength(200).IsRequired().IsUnicode();
            produkty.Property(x => x.Grupa).HasMaxLength(50).IsRequired().IsUnicode();
            produkty.Property(x => x.Stan).IsRequired();
            produkty.Property(x => x.CenaZakupu).IsRequired();
            produkty.Property(x => x.CenaSprzedazy).IsRequired();
            produkty.ToTable("Produkty");
        }
    }
}
