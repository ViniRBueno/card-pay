using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class UpdateLoanModel
    {
        public int loanId { get; set; }
        public bool approved { get; set; }
        public string reason { get; set; }
    }
}
