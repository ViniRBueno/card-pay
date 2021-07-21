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
        public string login { get; set; }
        public string password { get; set; }

        public User Convert(UserModel userModel)
        {
            var user = new User();

            user.login = userModel.login;
            user.password = userModel.password;
            user.user_name = userModel.user_name;
            user.cpf = userModel.cpf;

            return user;
        }
        public User() { }
    }
}
