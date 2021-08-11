using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class StatusHistoryMap : IEntityTypeConfiguration<StatusHistory>
    {
        public void Configure(EntityTypeBuilder<StatusHistory> builder)
        {

            builder.ToTable("Tb_StatusHistory");

            builder.HasKey(x => x.id_statushistory);
            builder.Property(x => x.id_statushistory).HasColumnName("id_statushistory");

            builder.Property(x => x.id_debt).HasColumnName("id_debt");

            builder.Property(x => x.id_status).HasColumnName("id_status");

            builder.Property(x => x.date_status).HasColumnName("date_status");
        }
    }
}
