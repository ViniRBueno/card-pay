using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class PaymentMap : EntityTypeConfiguration<Payment>
    {
        public PaymentMap()
        {
            ToTable("Tb_Payment");

            HasKey(x => x.id_payment).Property(x => x.id_payment).HasColumnName("id_payment");

            Property(x => x.id_debt).HasColumnName("id_debt");

            Property(x => x.id_paymenttype).HasColumnName("id_paymenttype");

            Property(x => x.payment_value).HasColumnName("payment_value");

            Property(x => x.status).HasColumnName("status");
        }
    }
}
