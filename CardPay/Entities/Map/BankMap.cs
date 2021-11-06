using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class BankMap : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.ToTable("Tb_Bank");

            builder.HasKey(x => x.id_bank);
            builder.Property(x => x.id_bank).HasColumnName("id_bank");

            builder.Property(x => x.bank_name).HasColumnName("bank_name");
        }
    }
}
