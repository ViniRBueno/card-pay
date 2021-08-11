using CardPay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardPay.Entities
{
    public partial class User
    {
        public int id_user { get; set; }
        public string user_name { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime birth_date { get; set; }

        public User Convert(UserModel userModel)
        {
            var user = new User();

            user.email = userModel.email;
            user.password = userModel.password;
            user.user_name = userModel.user_name;
            user.cpf = userModel.cpf;
            user.birth_date = userModel.birth_date;

            return user;
        }
        public User() { }
    }
}
