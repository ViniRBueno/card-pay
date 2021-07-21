using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class ReciptMap : EntityTypeConfiguration<Recipt>
    {
        public ReciptMap()
        {

            ToTable("Tb_Recipt");

            HasKey(x => x.id_recipt).Property(x => x.id_recipt).HasColumnName("id_recipt");

            Property(x => x.id_ticket).HasColumnName("id_ticket");

            Property(x => x.recipt_value).HasColumnName("recipt_value");
        }
    }
}
