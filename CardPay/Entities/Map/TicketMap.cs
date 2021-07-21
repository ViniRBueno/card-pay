using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class TicketMap : EntityTypeConfiguration<Ticket>
    {
        public TicketMap()
        {

            ToTable("Tb_Ticket");

            HasKey(x => x.id_ticket).Property(x => x.id_ticket).HasColumnName("id_ticket");

            Property(x => x.id_debt).HasColumnName("id_debt");

            Property(x => x.ticket_value).HasColumnName("ticket_value");

            Property(x => x.ticket_data).HasColumnName("ticket_data");

            Property(x => x.blocked).HasColumnName("blocked");
        }
    }
}
