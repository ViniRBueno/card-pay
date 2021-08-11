using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class StateMap : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("Tb_State");

            builder.HasKey(x => x.id_state);
            builder.Property(x => x.id_state).HasColumnName("id_state");

            builder.Property(x => x.name_state).HasColumnName("name_state");
        }
    }
}
