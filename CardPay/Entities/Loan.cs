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
        public Loan(CreateLoanModel loanModel)
        {
            id_family = loanModel.id_family;
            loan_value = loanModel.loan_value;
            total_parcels = loanModel.total_parcels;
            id_loanstatus = 1;
            create_date = DateTime.Now;
            parcel_value = loanModel.parcel_value;
            parcel_amount = loanModel.total_parcels;
        }
    }
}
