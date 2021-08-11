using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class DebtMap : IEntityTypeConfiguration<Debt>
    {
        public void Configure(EntityTypeBuilder<Debt> builder)
        {

            builder.ToTable("Tb_Debt");

            builder.HasKey(x => x.id_debt);
            builder.Property(x => x.id_debt).HasColumnName("id_debt");

            builder.Property(x => x.id_user).HasColumnName("id_user");

            builder.Property(x => x.id_status).HasColumnName("id_status");

            builder.Property(x => x.debt_value).HasColumnName("debt_value");
        }
    }
}
