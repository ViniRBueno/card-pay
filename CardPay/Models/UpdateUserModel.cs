using System;

namespace CardPay.Models
{
    public class UpdateUserModel
    {
        public string user_name { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public DateTime birth_date { get; set; }
    }
}
