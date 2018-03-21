using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PrisonApplication.Models.Identity;

namespace PrisonApplication.Models
{
    

    public partial class PrisonDatabase : IdentityDbContext<ApplicationUser>
    {
        public PrisonDatabase()
            : base("name=PrisonDatabase")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Allocation> Allocation { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<BranchesSex> BranchesSex { get; set; }
        public virtual DbSet<CategoriesOfCrimes> CategoriesOfCrimes { get; set; }
        public virtual DbSet<Cells> Cells { get; set; }
        public virtual DbSet<ChangesHistory> ChangesHistory { get; set; }
        public virtual DbSet<Judgements> Judgements { get; set; }
        public virtual DbSet<Prisoners> Prisoners { get; set; }
        public virtual DbSet<AllocationChangesHistory> AllocationChangesHistory { get; set; }
        public virtual DbSet<JudgementsChangesHistory> JudgementsChangesHistory { get; set; }
        public virtual DbSet<PrisonerChangesHistory> PrisonerChangesHistory { get; set; }
       

        public static PrisonDatabase Create()
        {
            return new PrisonDatabase();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

           
            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new {r.RoleId, r.UserId});

            modelBuilder.Entity<Allocation>()
                .HasMany(e => e.AllocationChangesHistory)
                .WithRequired(e => e.Allocation)
                .HasForeignKey(e => e.FK_AllocationChangesHistory_Allocation_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branches>()
                .HasOptional(e => e.BranchesSex)
                .WithRequired(e => e.Branches);

            modelBuilder.Entity<Branches>()
                .HasMany(e => e.Cells)
                .WithRequired(e => e.Branches)
                .HasForeignKey(e => e.FK_Cells_Branch)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoriesOfCrimes>()
                .Property(e => e.NameOfCategory)
                .IsUnicode(false);

            modelBuilder.Entity<CategoriesOfCrimes>()
                .HasMany(e => e.BranchesSex)
                .WithRequired(e => e.CategoriesOfCrimes)
                .HasForeignKey(e => e.FK_BranchesSex_CategoriesOfCrimes_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CategoriesOfCrimes>()
                .HasMany(e => e.Judgements)
                .WithRequired(e => e.CategoriesOfCrimes)
                .HasForeignKey(e => e.FK_Judgements_CategoriesOfCrimes_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cells>()
                .HasMany(e => e.Allocation)
                .WithRequired(e => e.Cells)
                .HasForeignKey(e => e.FK_Allocation_Cells_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChangesHistory>()
                .Property(e => e.Worker_Id)
                .IsUnicode(false);

            modelBuilder.Entity<ChangesHistory>()
                .HasMany(e => e.AllocationChangesHistory)
                .WithRequired(e => e.ChangesHistory)
                .HasForeignKey(e => e.FK_AllocationChangesHistory_ChangesHistory_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChangesHistory>()
                .HasMany(e => e.JudgementsChangesHistory)
                .WithRequired(e => e.ChangesHistory)
                .HasForeignKey(e => e.FK_JudgementsChangesHistory_ChangesHistory_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChangesHistory>()
                .HasMany(e => e.PrisonerChangesHistory)
                .WithRequired(e => e.ChangesHistory)
                .HasForeignKey(e => e.FK_PrisonerChangesHistory_ChangesHistory_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Judgements>()
                .HasMany(e => e.JudgementsChangesHistory)
                .WithRequired(e => e.Judgements)
                .HasForeignKey(e => e.FK_JudgementsChangesHistory_Judgements)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prisoners>()
                .Property(e => e.PrisonerName)
                .IsUnicode(false);

            modelBuilder.Entity<Prisoners>()
                .Property(e => e.PrisonerSurname)
                .IsUnicode(false);

            modelBuilder.Entity<Prisoners>()
                .Property(e => e.Pesel)
                .IsUnicode(false);

            modelBuilder.Entity<Prisoners>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<Prisoners>()
                .HasOptional(e => e.Allocation)
                .WithRequired(e => e.Prisoners);

            modelBuilder.Entity<Prisoners>()
                .HasMany(e => e.Judgements)
                .WithRequired(e => e.Prisoners)
                .HasForeignKey(e => e.FK_Judgements_Prisoners_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prisoners>()
                .HasMany(e => e.PrisonerChangesHistory)
                .WithRequired(e => e.Prisoners)
                .HasForeignKey(e => e.FK_PrisonerChangesHistory_Prisoner_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PrisonerChangesHistory>()
                .Property(e => e.Pesel)
                .IsUnicode(false);

            modelBuilder.Entity<PrisonerChangesHistory>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<PrisonerChangesHistory>()
                .Property(e => e.PrisonerName)
                .IsUnicode(false);

            modelBuilder.Entity<PrisonerChangesHistory>()
                .Property(e => e.PrisonerSurname)
                .IsUnicode(false);
        }
    }
}
