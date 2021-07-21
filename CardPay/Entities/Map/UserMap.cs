using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace CardPay.Entities.Map
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Tb_User");

            HasKey(x => x.id_user).Property(x => x.id_user).HasColumnName("id_user");

            Property(x => x.user_name).HasColumnName("user_name");

            Property(x => x.cpf).HasColumnName("cpf");

            Property(x => x.login).HasColumnName("email");

            Property(x => x.password).HasColumnName("password");

            Property(x => x.password).HasColumnName("password");

        }
    }
}
