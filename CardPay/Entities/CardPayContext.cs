using Microsoft.EntityFrameworkCore;

namespace CardPay.Entities
{
    public class CardPayContext : DbContext
    {
        public CardPayContext(DbContextOptions<CardPayContext> contextOptions) : base(contextOptions)
        { }

        public DbSet<User> users { get; set; }
        public DbSet<Status> statuses { get; set; }
        public DbSet<Account> accounts { get; set; }
        public DbSet<Bank> banks { get; set; }
        public DbSet<Family> families { get; set; }
        public DbSet<FamilyMember> familyMembers { get; set; }
        public DbSet<Loan> loans { get; set; }
        public DbSet<LoanStatus> loanstatuses { get; set; }
        public DbSet<Parcel> parcels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(new Map.UserMap().Configure);
            modelBuilder.Entity<Status>(new Map.StatusMap().Configure);
            modelBuilder.Entity<Account>(new Map.AccountMap().Configure);
            modelBuilder.Entity<Bank>(new Map.BankMap().Configure);
            modelBuilder.Entity<Family>(new Map.FamilyMap().Configure);
            modelBuilder.Entity<FamilyMember>(new Map.FamilyMemberMap().Configure);
            modelBuilder.Entity<Loan>(new Map.LoanMap().Configure);
            modelBuilder.Entity<LoanStatus>(new Map.LoanStatusMap().Configure);
            modelBuilder.Entity<Parcel>(new Map.ParcelMap().Configure);
        }
    }
}
