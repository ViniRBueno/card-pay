using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities
{
    public class Address
    {
        public int id_adress { get; set; }
        public int id_user { get; set; }
        public int id_state { get; set; }
        public string street { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
    }
}
