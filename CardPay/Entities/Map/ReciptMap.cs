using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class ReciptMap : IEntityTypeConfiguration<Recipt>
    {
        public void Configure(EntityTypeBuilder<Recipt> builder)
        {
            builder.ToTable("Tb_Recipt");

            builder.HasKey(x => x.id_recipt);
            builder.Property(x => x.id_recipt).HasColumnName("id_recipt");

            builder.Property(x => x.id_ticket).HasColumnName("id_ticket");

            builder.Property(x => x.recipt_value).HasColumnName("recipt_value");
        }
    }
}
