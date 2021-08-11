using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Tb_Address");

            builder.HasKey(x => x.id_adress);
            builder.Property(x => x.id_adress).HasColumnName("id_adress");

            builder.Property(x => x.id_user).HasColumnName("id_user");

            builder.Property(x => x.id_state).HasColumnName("id_state");

            builder.Property(x => x.street).HasColumnName("street");

            builder.Property(x => x.district).HasColumnName("district");

            builder.Property(x => x.city).HasColumnName("city");

            builder.Property(x => x.country).HasColumnName("country");

            builder.Property(x => x.number).HasColumnName("number");

            builder.Property(x => x.complement).HasColumnName("complement");
        }
    }
}
