using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class PhoneTypeMap : EntityTypeConfiguration<PhoneType>
    {
        public PhoneTypeMap()
        {
            ToTable("Tb_PhoneType");

            HasKey(x => x.id_phonetype).Property(x => x.id_phonetype).HasColumnName("id_phonetype");

            Property(x => x.name_phonetype).HasColumnName("name_phonetype");
        }
    }
}
