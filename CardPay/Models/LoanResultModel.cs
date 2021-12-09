using CardPay.Enums;

namespace CardPay.Models
{
    public class LoanResultModel
    {
        public LoanResultEnum loanStatusId { get; set; }
        public string statusDescription { get; set; }
        public string reason { get; set; }
        public LoanEstimateModel loanEstimate { get; set; }
        public LoanWithParcelsModel loanModel { get; set; }
    }
}
