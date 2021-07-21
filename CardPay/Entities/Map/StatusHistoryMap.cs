using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class StatusHistoryMap : EntityTypeConfiguration<StatusHistory>
    {
        public StatusHistoryMap()
        {

            ToTable("Tb_StatusHistory");

            HasKey(x => x.id_statushistory).Property(x => x.id_statushistory).HasColumnName("id_statushistory");

            Property(x => x.id_debt).HasColumnName("id_debt");

            Property(x => x.id_status).HasColumnName("id_status");

            Property(x => x.date_status).HasColumnName("date_status");
        }
    }
}
