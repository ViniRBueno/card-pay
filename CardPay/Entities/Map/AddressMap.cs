using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities.Map
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            ToTable("Tb_Address");

            HasKey(x => x.id_adress).Property(x => x.id_adress).HasColumnName("id_adress");

            Property(x => x.id_user).HasColumnName("id_user");
            
            Property(x => x.id_state).HasColumnName("id_state");
            
            Property(x => x.street).HasColumnName("street");
            
            Property(x => x.district).HasColumnName("district");
            
            Property(x => x.city).HasColumnName("city");
            
            Property(x => x.country).HasColumnName("country");
           
            Property(x => x.number).HasColumnName("number");
            
            Property(x => x.complement).HasColumnName("complement");
        }
    }
}
