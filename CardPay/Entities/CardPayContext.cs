using Microsoft.EntityFrameworkCore;

namespace CardPay.Entities
{
    public class CardPayContext : DbContext
    {
        public CardPayContext(DbContextOptions<CardPayContext> contextOptions) : base(contextOptions)
        { }

        public DbSet<Debt> debts { get; set; }
        public DbSet<DebtType> debtTypes { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<PaymentType> paymentTypes { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<Phone> phones { get; set; }
        public DbSet<PhoneType> phoneTypes { get; set; }
        public DbSet<Recipt> recipts { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<Status> statuses { get; set; }
        public DbSet<StatusHistory> statusHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Debt>(new Map.DebtMap().Configure);
            modelBuilder.Entity<Address>(new Map.AddressMap().Configure);
            modelBuilder.Entity<Payment>(new Map.PaymentMap().Configure);
            modelBuilder.Entity<PaymentType>(new Map.PaymentTypeMap().Configure);
            modelBuilder.Entity<Phone>(new Map.PhoneMap().Configure);
            modelBuilder.Entity<PhoneType>(new Map.PhoneTypeMap().Configure);
            modelBuilder.Entity<Recipt>(new Map.ReciptMap().Configure);
            modelBuilder.Entity<State>(new Map.StateMap().Configure);
            modelBuilder.Entity<StatusHistory>(new Map.StatusHistoryMap().Configure);
            modelBuilder.Entity<User>(new Map.UserMap().Configure);
            modelBuilder.Entity<Status>(new Map.StatusMap().Configure);
            modelBuilder.Entity<Ticket>(new Map.TicketMap().Configure);

            //modelBuilder.Configurations.Add(new Map.TicketMap());

        }
    }
}
