namespace CardPay.Entities
{
    public partial class Admin
    {
        public int id_admin { get; set; }
        public string name_admin { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public Admin() { }
    }
}
