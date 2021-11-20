namespace CardPay.Models
{
    public class CreateLoanModel
    {
        public int id_family { get; set; }
        public decimal loan_value { get; set; }
        public int total_parcels { get; set; }
    }
}
