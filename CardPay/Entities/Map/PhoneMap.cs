using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class PhoneMap : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("Tb_Phone");

            builder.HasKey(x => x.id_phone);
            builder.Property(x => x.id_phone).HasColumnName("id_phone");

            builder.Property(x => x.id_user).HasColumnName("id_user");

            builder.Property(x => x.id_phonetype).HasColumnName("id_phonetype");

            builder.Property(x => x.phone_number).HasColumnName("phone_number");
        }
    }
}
