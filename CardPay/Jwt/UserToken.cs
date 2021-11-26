using CardPay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Jwt
{
    public class UserToken
    {
        public UserToken() { } //não tira essa porra
        public UserToken(User user)
        {
            this.id = user.id_user;
            this.email = user.email;
            this.user_name = user.user_name;
        }
        public int id { get; set; }
        public string email { get; set; }
        public string user_name { get; set; }
    }
}
