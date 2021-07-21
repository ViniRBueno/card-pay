using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class StatusMap : EntityTypeConfiguration<Status>
    {
        public StatusMap()
        {
            ToTable("Tb_Status");

            HasKey(x => x.id_status).Property(x => x.id_status).HasColumnName("id_status");

            Property(x => x.name_status).HasColumnName("name_status");

            Property(x => x.status_description).HasColumnName("status_description");
        }
    }
}
