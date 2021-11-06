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
        }
    }
}
