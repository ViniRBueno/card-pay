using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class StatusMap : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Tb_Status");

            builder.HasKey(x => x.id_status);
            builder.Property(x => x.id_status).HasColumnName("id_status");

            builder.Property(x => x.name_status).HasColumnName("name_status");

            builder.Property(x => x.status_description).HasColumnName("status_description");
        }
    }
}
