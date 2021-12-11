using CardPay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class LoanInfoModel
    {
        public Family family { get; set; }
        public List<FamilyMember> familyMembers { get; set; }
        public Loan loan { get; set; }
    }
}
