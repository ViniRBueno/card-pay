using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities
{
    public class Phone
    {
        public int id_phone { get; set; }
        public int id_user { get; set; }
        public int id_phonetype { get; set; }
        public string phone_number { get; set; }
    }
}
