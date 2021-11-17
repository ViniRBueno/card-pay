using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Tb_Account");

            builder.HasKey(x => x.id_account);
            builder.Property(x => x.id_account).HasColumnName("id_account");

            builder.Property(x => x.id_user).HasColumnName("id_user");

            builder.Property(x => x.id_bank).HasColumnName("id_bank");

            builder.Property(x => x.agency).HasColumnName("agency");

            builder.Property(x => x.account).HasColumnName("account");

            builder.Property(x => x.active).HasColumnName("active");
        }
    }
}
