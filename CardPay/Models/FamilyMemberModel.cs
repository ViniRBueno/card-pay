using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class FamilyMemberModel
    {
        public int id_family { get; set; }
        public string cpf { get; set; }
        public string member_name { get; set; }
        public decimal salary { get; set; }
    }
}
