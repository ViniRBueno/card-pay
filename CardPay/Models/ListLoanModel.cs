using CardPay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class ListLoanModel
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public Loan loan { get; set; }
        public List<FamilyMember> familyMembers { get; set; }
        public List<Parcel> parcels { get; set; }
    }
}
