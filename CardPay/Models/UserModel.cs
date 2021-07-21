using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class UserModel
    {
        public string user_name { get; set; }
        public string cpf { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
