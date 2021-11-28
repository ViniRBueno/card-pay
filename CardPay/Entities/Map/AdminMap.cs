using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class AdminMap : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Tb_Admin");

            builder.HasKey(x => x.id_admin);
            builder.Property(x => x.id_admin).HasColumnName("id_admin");

            builder.Property(x => x.name_admin).HasColumnName("name_admin");

            builder.Property(x => x.email).HasColumnName("email");

            builder.Property(x => x.password).HasColumnName("password");
        }
    }
}
