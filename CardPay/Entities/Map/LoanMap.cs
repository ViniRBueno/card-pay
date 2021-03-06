using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class LoanMap : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Tb_Loan");

            builder.HasKey(x => x.id_loan);
            builder.Property(x => x.id_loan).HasColumnName("id_loan");

            builder.Property(x => x.id_family).HasColumnName("id_family");

            builder.Property(x => x.loan_value).HasColumnName("loan_value");

            builder.Property(x => x.total_parcels).HasColumnName("total_parcels");

            builder.Property(x => x.id_loanstatus).HasColumnName("id_loanstatus");

            builder.Property(x => x.reason).HasColumnName("reason");

            builder.Property(x => x.create_date).HasColumnName("create_date");

            builder.Property(x => x.parcel_value).HasColumnName("parcel_value");

            builder.Property(x => x.parcel_amount).HasColumnName("parcel_amount");
        }
    }
}
