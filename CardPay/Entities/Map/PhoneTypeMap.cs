using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class PhoneTypeMap : IEntityTypeConfiguration<PhoneType>
    {
        public void Configure(EntityTypeBuilder<PhoneType> builder)
        {
            builder.ToTable("Tb_PhoneType");

            builder.HasKey(x => x.id_phonetype);
            builder.Property(x => x.id_phonetype).HasColumnName("id_phonetype");

            builder.Property(x => x.name_phonetype).HasColumnName("name_phonetype");
        }
    }
}
