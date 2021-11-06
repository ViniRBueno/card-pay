using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class FamilyMap : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.ToTable("Tb_Family");

            builder.HasKey(x => x.id_family);
            builder.Property(x => x.id_family).HasColumnName("id_family");

            builder.Property(x => x.id_user).HasColumnName("id_user");
        }
    }
}
