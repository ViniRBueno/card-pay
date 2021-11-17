using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class AccountModel
    {
        public int id_bank { get; set; }
        public string agency { get; set; }
        public string account { get; set; }
    }
}
