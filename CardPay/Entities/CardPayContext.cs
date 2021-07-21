using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CardPay.Entities
{
    public class CardPayContext : DbContext
    {
        public CardPayContext() : base("CardPayContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Account> accounts { get; set; }
        public DbSet<Bank> banks { get; set; }
        public DbSet<Debt> debts { get; set; }
        public DbSet<DebtType> debtTypes { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<PaymentType> paymentTypes { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new Map.AccountMap());
            modelBuilder.Configurations.Add(new Map.BankMap());
            modelBuilder.Configurations.Add(new Map.DebtMap());
            modelBuilder.Configurations.Add(new Map.DebtTypeMap());
            modelBuilder.Configurations.Add(new Map.OrderMap());
            modelBuilder.Configurations.Add(new Map.PaymentMap());
            modelBuilder.Configurations.Add(new Map.PaymentTypeMap());
            modelBuilder.Configurations.Add(new Map.UserMap());

        }
    }
}
