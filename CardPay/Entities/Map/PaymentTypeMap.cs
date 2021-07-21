using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class PaymentTypeMap : EntityTypeConfiguration<PaymentType>
    {
        public PaymentTypeMap()
        {
            ToTable("Tb_PaymentType");

            HasKey(x => x.id_paymenttype).Property(x => x.id_paymenttype).HasColumnName("id_paymenttype");

            Property(x => x.paymenttype_name).HasColumnName("paymenttype_name");
        }
    }
}
