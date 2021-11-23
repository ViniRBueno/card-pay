using CardPay.Models;

namespace CardPay.Entities
{
    public partial class Loan
    {
        public int id_loan { get; set; }
        public int id_family { get; set; }
        public decimal loan_value { get; set; }
        public int total_parcels { get; set; }
        public int id_loanstatus { get; set; }

        public Loan() { }
        public Loan(CreateLoanModel loanModel)
        {
            id_family = loanModel.id_family;
            loan_value = loanModel.loan_value;
            total_parcels = loanModel.total_parcels;
            id_loanstatus = 1;
        }
    }
}
