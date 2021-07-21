using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class PhoneMap : EntityTypeConfiguration<Phone>
    {
        public PhoneMap()
        {
            ToTable("Tb_Phone");

            HasKey(x => x.id_phone).Property(x => x.id_phone).HasColumnName("id_phone");

            Property(x => x.id_user).HasColumnName("id_user");

            Property(x => x.id_phonetype).HasColumnName("id_phonetype");

            Property(x => x.phone_number).HasColumnName("phone_number");
        }
    }
}
