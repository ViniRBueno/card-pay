using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class TicketMap : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tb_Ticket");

            builder.HasKey(x => x.id_ticket);
            builder.Property(x => x.id_ticket).HasColumnName("id_ticket");

            builder.Property(x => x.id_debt).HasColumnName("id_debt");

            builder.Property(x => x.ticket_value).HasColumnName("ticket_value");

            builder.Property(x => x.ticket_data).HasColumnName("ticket_data");

            builder.Property(x => x.blocked).HasColumnName("blocked");
        }
    }
}
