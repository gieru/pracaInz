using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using projektInz.biznes;
using System.Data.Entity;

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
        public DbSet<Klient> Klienci { get; set; }
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

            var klienci = modelBuilder.Entity<Klient>();
            klienci.HasKey(x => x.Id);
            klienci.Property(x => x.Imie).HasMaxLength(50);
            klienci.Property(x => x.Nazwisko).HasMaxLength(75);
            klienci.Property(x => x.Pesel);
            klienci.Property(x => x.Nip);
            klienci.Property(x => x.NazwaFirmy);
            klienci.Property(x => x.Adres).IsRequired();
            klienci.Property(x => x.NrTel);
            klienci.Property(x => x.Email);
            klienci.ToTable("Klienci");


            var produkty = modelBuilder.Entity<Produkt>();
            produkty.HasKey(x => x.Id);
            produkty.Property(x => x.Nazwa).HasMaxLength(200).IsRequired().IsUnicode();
            produkty.Property(x => x.Grupa).HasMaxLength(200).IsRequired().IsUnicode();
            produkty.Property(x => x.Stan).IsRequired();
            produkty.Property(x => x.Marza).IsRequired();
            produkty.Property(x => x.StawkaVat).IsRequired();
            produkty.Property(x => x.CenaZakupuNetto).IsRequired();
            produkty.ToTable("Produkty");

            var zamowienia = modelBuilder.Entity<Zamowienie>();
            zamowienia.ToTable("Zamowienia");
            zamowienia.HasKey(x => x.Id);
            zamowienia.HasMany(x => x.Pozycje);
            zamowienia.HasRequired(x => x.Klient);

            var pozycjeZamowien = modelBuilder.Entity<PozycjaZamowienia>();
            pozycjeZamowien.HasRequired(p => p.Produkt).WithMany();
            pozycjeZamowien.ToTable("PozycjeZamowienia");

            var faktury = modelBuilder.Entity<Faktura>();
            faktury.ToTable("Faktury");
            faktury.HasKey(x => x.Id);
            faktury.HasMany(x => x.Pozycje);
            faktury.HasRequired(x => x.Klient);

            var pozycjeFaktur = modelBuilder.Entity<PozycjaFaktury>();
            pozycjeFaktur.HasRequired(p => p.Produkt).WithMany();
            pozycjeFaktur.ToTable("PozycjeFaktury");
        }
    }
}
