using System;

namespace CardPay.Entities
{
    public partial class Parcel
    {
        public int id_parcel { get; set; }
        public int id_loan { get; set; }
        public decimal parcel_value { get; set; }
        public int id_status { get; set; }
        public int parcel_number { get; set; }
        public string ticket_number { get; set; }
        public DateTime expire_date { get; set; }
    }
}
