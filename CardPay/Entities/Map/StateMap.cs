using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class StateMap : EntityTypeConfiguration<State>
    {
        public StateMap()
        {
            ToTable("Tb_State");

            HasKey(x => x.id_state).Property(x => x.id_state).HasColumnName("id_state");

            Property(x => x.name_state).HasColumnName("name_state");
        }
    }
}
