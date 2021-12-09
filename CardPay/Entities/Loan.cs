using CardPay.Models;
using System;

namespace CardPay.Entities
{
    public partial class Loan
    {
        public int id_loan { get; set; }
        public int id_family { get; set; }
        public decimal loan_value { get; set; }
        public int total_parcels { get; set; }
        public int id_loanstatus { get; set; }
        public string reason { get; set; }
        public DateTime create_date { get; set; }
        public decimal parcel_value { get; set; }
        public int parcel_amount { get; set; }

        public Loan() { }
        public Loan(decimal loanValue, int familyId)
        {
            id_family = familyId;
            loan_value = loanValue;
            total_parcels = 36;
            id_loanstatus = 1;
            create_date = DateTime.Now;
            parcel_value = (loanValue * 1.05M) / 36;
            parcel_amount = 36;
        }
    }
}
