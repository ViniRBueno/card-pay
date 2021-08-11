using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace CardPay.Entities.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Tb_User");

            builder.HasKey(x => x.id_user);
            builder.Property(x => x.id_user).HasColumnName("id_user");

            builder.Property(x => x.user_name).HasColumnName("user_name");

            builder.Property(x => x.cpf).HasColumnName("cpf");

            builder.Property(x => x.email).HasColumnName("email");

            builder.Property(x => x.password).HasColumnName("password");

            builder.Property(x => x.birth_date).HasColumnName("birth_date");
        }
    }
}
