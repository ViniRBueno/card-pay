using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class PaymentTypeMap : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.ToTable("Tb_PaymentType");

            builder.HasKey(x => x.id_paymenttype);
            builder.Property(x => x.id_paymenttype).HasColumnName("id_paymenttype");

            builder.Property(x => x.paymenttype_name).HasColumnName("paymenttype_name");
        }
    }
}
