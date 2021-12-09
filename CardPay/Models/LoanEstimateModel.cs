using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class LoanEstimateModel
    {
        public decimal loan_value { get; set; }
        public int total_parcels { get; set; }
        public decimal parcel_value { get; set; }
        public decimal fee { get; set; }

        public LoanEstimateModel() { }
    }
}
