using CardPay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class LoanWithParcelsModel
    {
        public Loan loan { get; set; }
        public IEnumerable<Parcel> parcels { get; set; }
    }
}
