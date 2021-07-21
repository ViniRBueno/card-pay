using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Entities
{
    public class StatusHistory
    {
        public int id_statushistory { get; set; }
        public int id_debt { get; set; }
        public int id_status { get; set; }
        public DateTime date_status { get; set; }
    }
}
