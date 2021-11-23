using CardPay.Models;

namespace CardPay.Entities
{
    public partial class Account
    {
        public Account() { }
        public Account(AccountModel accountModel, int UserId)
        {
            id_user = UserId;
            id_bank = accountModel.id_bank;
            agency = accountModel.agency;
            account = accountModel.account;
            active = true;
        }
        public int id_account { get; set; }
        public int id_user { get; set; }
        public int id_bank { get; set; }
        public string agency { get; set; }
        public string account { get; set; }
        public bool active { get; set; }
    }
}
