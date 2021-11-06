using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class FamilyMemberMap : IEntityTypeConfiguration<FamilyMember>
    {
        public void Configure(EntityTypeBuilder<FamilyMember> builder)
        {
            builder.ToTable("Tb_FamilyMember");

            builder.HasKey(x => x.id_member);
            builder.Property(x => x.id_member).HasColumnName("id_member");

            builder.Property(x => x.id_family).HasColumnName("id_family");

            builder.Property(x => x.cpf).HasColumnName("cpf");

            builder.Property(x => x.member_name).HasColumnName("member_name");

            builder.Property(x => x.salary).HasColumnName("salary");
        }
    }
}
