using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class PaymentMap : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Tb_Payment");

            builder.HasKey(x => x.id_payment);
            builder.Property(x => x.id_payment).HasColumnName("id_payment");

            builder.Property(x => x.id_debt).HasColumnName("id_debt");

            builder.Property(x => x.id_paymenttype).HasColumnName("id_paymenttype");

            builder.Property(x => x.payment_value).HasColumnName("payment_value");

            builder.Property(x => x.status).HasColumnName("status");
        }
    }
}
