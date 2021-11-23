namespace CardPay.Entities
{
    public partial class Family
    {
        public int id_family { get; set; }
        public int id_user { get; set; }
        public decimal total_salary { get; set; }

        public Family () { }
        public Family (int id)
        {
            id_user = id;
            total_salary = 0.00M;
        }
    }
}
