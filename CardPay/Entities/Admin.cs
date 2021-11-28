namespace CardPay.Entities
{
    public partial class Admin
    {
        public int id_admin { get; set; }
        public int name_admin { get; set; }
        public decimal email { get; set; }
        public int password { get; set; }

        public Admin() { }
    }
}
