using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class DebtMap : EntityTypeConfiguration<Debt>
    {
        public DebtMap()
        {

            ToTable("Tb_Debt");

            HasKey(x => x.id_debt).Property(x => x.id_debt).HasColumnName("id_debt");

            Property(x => x.id_user).HasColumnName("id_user");

            Property(x => x.id_status).HasColumnName("id_status");

            Property(x => x.debt_value).HasColumnName("debt_value");
        }
    }
}
