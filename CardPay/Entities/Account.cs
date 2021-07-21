using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities
{
    public partial class Account
    {
        public int id_account { get; set; }
        public int id_user { get; set; }
        public int id_bank { get; set; }
        public string account_number { get; set; }
        public int account_branch { get; set; }
    }
}
