using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PrisonApp.Models.Identity;

namespace PrisonApp.Models
{
    public class Model : IdentityDbContext<AppUser>
    {
        public Model()
            : base("name=Model")
        {
            Configuration.LazyLoadingEnabled = true;
        }


        public virtual DbSet<Cele> Cele { get; set; }
        public virtual DbSet<HistoriaZmian> HistoriaZmian { get; set; }
        public virtual DbSet<KategoriePrzestepstwa> KategoriePrzestepstwa { get; set; }
        public virtual DbSet<Odzial> Odzial { get; set; }
        public virtual DbSet<Przydzialy> Przydzialy { get; set; }
        public virtual DbSet<Wiezniowie> Wiezniowie { get; set; }
        public virtual DbSet<Wyroki> Wyroki { get; set; }
        public virtual DbSet<HistoriaZmianPrzydzialow> HistoriaZmianPrzydzialow { get; set; }
        public virtual DbSet<HistoriaZmianWieznia> HistoriaZmianWieznia { get; set; }
        public virtual DbSet<HistoriaZmianWyrokow> HistoriaZmianWyrokow { get; set; }
        public virtual DbSet<KategoriaPrzestepstwaOdzial> KategoriaPrzestepstwaOdzial { get; set; }

        public static Model Create()
        {
            return new Model();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new {r.RoleId, r.UserId});
            modelBuilder.Entity<Cele>()
                .HasMany(e => e.Przydzialy)
                .WithRequired(e => e.Cele)
                .HasForeignKey(e => e.FK_idCeli)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HistoriaZmian>()
                .HasMany(e => e.HistoriaZmianPrzydzialow)
                .WithRequired(e => e.HistoriaZmian)
                .HasForeignKey(e => e.FK_HistoriaZmian_idWpisuPrzydzialu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HistoriaZmian>()
                .HasMany(e => e.HistoriaZmianWieznia)
                .WithRequired(e => e.HistoriaZmian)
                .HasForeignKey(e => e.FK_HistoriaZmian_idWpisuWieznia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HistoriaZmian>()
                .HasMany(e => e.HistoriaZmianWyrokow)
                .WithRequired(e => e.HistoriaZmian)
                .HasForeignKey(e => e.FK_HistoriaZmian_idWpisu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KategoriePrzestepstwa>()
                .Property(e => e.NazwaKategorii)
                .IsUnicode(false);

            modelBuilder.Entity<KategoriePrzestepstwa>()
                .HasMany(e => e.KategoriaPrzestepstwaOdzial)
                .WithRequired(e => e.KategoriePrzestepstwa)
                .HasForeignKey(e => e.FK_idKategoriiPrzestepstwa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KategoriePrzestepstwa>()
                .HasMany(e => e.Wyroki)
                .WithRequired(e => e.KategoriePrzestepstwa)
                .HasForeignKey(e => e.FK_idKategoriiPrzestepstwa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Odzial>()
                .HasMany(e => e.Cele)
                .WithRequired(e => e.Odzial)
                .HasForeignKey(e => e.FK_idOdzial)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Odzial>()
                .HasMany(e => e.KategoriaPrzestepstwaOdzial)
                .WithRequired(e => e.Odzial)
                .HasForeignKey(e => e.FK_idOdzial)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Przydzialy>()
                .HasMany(e => e.HistoriaZmianPrzydzialow)
                .WithRequired(e => e.Przydzialy)
                .HasForeignKey(e => e.FK_idWiezniaPrzydzialu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Wiezniowie>()
                .Property(e => e.ImieWieznia)
                .IsUnicode(false);

            modelBuilder.Entity<Wiezniowie>()
                .Property(e => e.NazwiskoWieznia)
                .IsUnicode(false);

            modelBuilder.Entity<Wiezniowie>()
                .Property(e => e.Pesel)
                .IsUnicode(false);

            modelBuilder.Entity<Wiezniowie>()
                .Property(e => e.Plec)
                .IsUnicode(false);

            modelBuilder.Entity<Wiezniowie>()
                .HasOptional(e => e.Przydzialy)
                .WithRequired(e => e.Wiezniowie);

            modelBuilder.Entity<Wiezniowie>()
                .HasMany(e => e.HistoriaZmianWieznia)
                .WithRequired(e => e.Wiezniowie)
                .HasForeignKey(e => e.FK_idWieznia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Wiezniowie>()
                .HasMany(e => e.Wyroki)
                .WithRequired(e => e.Wiezniowie)
                .HasForeignKey(e => e.FK_idWieznia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Wyroki>()
                .HasMany(e => e.HistoriaZmianWyrokow)
                .WithRequired(e => e.Wyroki)
                .HasForeignKey(e => e.FK_idWyroku)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HistoriaZmianWieznia>()
                .Property(e => e.ImieWieznia)
                .IsUnicode(false);

            modelBuilder.Entity<HistoriaZmianWieznia>()
                .Property(e => e.NazwiskoWieznia)
                .IsUnicode(false);

            modelBuilder.Entity<HistoriaZmianWieznia>()
                .Property(e => e.Pesel)
                .IsUnicode(false);

            modelBuilder.Entity<HistoriaZmianWieznia>()
                .Property(e => e.Plec)
                .IsUnicode(false);

            modelBuilder.Entity<KategoriaPrzestepstwaOdzial>()
                .Property(e => e.PlecOdzial)
                .IsUnicode(false);
        }
    }
}