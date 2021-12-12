using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardPay.Entities.Map
{
    public class LoanStatusMap : IEntityTypeConfiguration<LoanStatus>
    {
        public void Configure(EntityTypeBuilder<LoanStatus> builder)
        {
            builder.ToTable("Tb_LoanStatus");

            builder.HasKey(x => x.id_loanstatus);
            builder.Property(x => x.id_loanstatus).HasColumnName("id_loanstatus");

            builder.Property(x => x.name_status).HasColumnName("name_status");

            builder.Property(x => x.status_description).HasColumnName("status_description");
        }
    }
}
