using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class ParcelMap : IEntityTypeConfiguration<Parcel>
    {
        public void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.ToTable("Tb_Parcel");

            builder.HasKey(x => x.id_parcel);
            builder.Property(x => x.id_parcel).HasColumnName("id_parcel");

            builder.Property(x => x.id_loan).HasColumnName("id_loan");

            builder.Property(x => x.parcel_value).HasColumnName("parcel_value");

            builder.Property(x => x.id_status).HasColumnName("id_status");

            builder.Property(x => x.parcel_number).HasColumnName("parcel_number");

            builder.Property(x => x.ticket_number).HasColumnName("ticket_number");
        }
    }
}
