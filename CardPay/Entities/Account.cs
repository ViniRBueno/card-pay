using CardPay.Models;
using System.ComponentModel.DataAnnotations.Schema;

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
            _active = 1;
        }
        public int id_account { get; set; }
        public int id_user { get; set; }
        public int id_bank { get; set; }
        public string agency { get; set; }
        public string account { get; set; }
        public int _active { get; set; }
        [NotMapped]
        public bool active
        {
            get
            {
                if (_active == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            set
            {   
                if (_active == 0)
                {
                    this.active = false;
                }
                else
                {
                    this.active = true;
                }
            }
        }
    }
}
