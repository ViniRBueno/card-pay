using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class UpdateUserModel
    {
        public decimal salary { get; set; }
        public AccountModel account { get; set; }
    }
}
