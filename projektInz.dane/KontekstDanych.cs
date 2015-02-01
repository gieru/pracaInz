using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Web.UI;
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
            Database.SetInitializer(new InicjalizatorBazyDanych());
        }

        public DbSet<Użytkownik> Użytkownicy { get; set; }
       // public DbSet<Użytkownik> Klienci { get; set; }
        public DbSet<Kontrahent> Kontrahenci { get; set; } 
        public DbSet<Produkt> Produkty { get; set; } 

        public DbSet<Zamowienie> Zamowienia { get; set; } 
        public DbSet<PozycjaZamowienia> PozycjeZamowien { get; set; } 

        public DbSet<Faktura> Faktury { get; set; }
        public DbSet<PozycjaFaktury> PozycjeFaktur { get; set; }

        public DbSet<GeneratorNumerowFaktur> GeneratoryNumerowFaktur { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var uzytkownicy = modelBuilder.Entity<Użytkownik>()
                .HasKey(x => x.Id)
                .ToTable("Uzytkownicy");

            uzytkownicy
                .Property(x => x.Login).HasMaxLength(30).IsRequired().HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Login", 1) {IsUnique = true}));

            var kontrahenci = modelBuilder.Entity<Kontrahent>();
            kontrahenci.HasKey(x => x.Id);
            kontrahenci.Property(x => x.Imię).HasColumnName("Imie").HasMaxLength(100).IsRequired().IsUnicode();
            kontrahenci.Property(x => x.Nazwisko).HasColumnName("Nazwisko").HasMaxLength(100).IsRequired().IsUnicode();
            kontrahenci.Property(x => x.NazwaFirmy)
                .HasColumnName("Nazwa Firmy")
                .HasMaxLength(250)
                .IsRequired()
                .IsUnicode();
            kontrahenci.Property(x => x.Nip).HasMaxLength(13).IsRequired();
            kontrahenci.Property(x => x.Adres).HasMaxLength(250).IsRequired();
            kontrahenci.Property(x => x.NrTel).HasMaxLength(20).IsRequired();
            kontrahenci.Property(x => x.Email).HasMaxLength(70).IsRequired();
            kontrahenci.ToTable("Kontrahenci");

          /*  var klienci = modelBuilder.Entity<Klienci>();
            klienci.HasKey(x => x.Id);
            klienci.HasKey(x => x.Imie);
            klienci.HasKey(x => x.Nazwisko);
            klienci.HasKey(x => x.PESEL);
            klienci.HasKey(x => x.Nip);
            klienci.HasKey(x => x.NazwaFirmy);
            klienci.HasKey(x => x.Adres);
            klienci.HasKey(x => x.NrTel);
            klienci.HasKey(x => x.Email);
            klienci.ToTable("Klienci"); */


            var produkty = modelBuilder.Entity<Produkt>();
            produkty.HasKey(x => x.Id);
            produkty.Property(x => x.Nazwa).HasMaxLength(200).IsRequired().IsUnicode();
            produkty.Property(x => x.Grupa).HasMaxLength(200).IsRequired().IsUnicode();
            produkty.Property(x => x.Stan).IsRequired();
            produkty.Property(x => x.CenaZakupu).IsRequired();
            produkty.Property(x => x.CenaSprzedazy).IsRequired();
            produkty.ToTable("Produkty");

            var zamowienia = modelBuilder.Entity<Zamowienie>();
            zamowienia.ToTable("Zamowienia");
            zamowienia.HasKey(x => x.Id);
            zamowienia.HasMany(x => x.Pozycje);

            var pozycjeZamowien = modelBuilder.Entity<PozycjaZamowienia>();
            pozycjeZamowien.HasRequired(p => p.Produkt).WithMany();
            pozycjeZamowien.ToTable("PozycjeZamowienia");

            var faktury = modelBuilder.Entity<Faktura>();
            faktury.ToTable("Faktury");
            faktury.HasKey(x => x.Id);
            faktury.HasMany(x => x.Pozycje);

            var pozycjeFaktur = modelBuilder.Entity<PozycjaFaktury>();
            pozycjeFaktur.HasRequired(p => p.Produkt).WithMany();
            pozycjeFaktur.ToTable("PozycjeFaktury");
        }
    }
}
