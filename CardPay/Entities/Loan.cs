namespace CardPay.Entities
{
    public partial class Loan
    {
        public int id_loan { get; set; }
        public int id_family { get; set; }
        public decimal loan_value { get; set; }
        public int total_parcels { get; set; }
    }
}
