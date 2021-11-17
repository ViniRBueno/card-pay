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
        public decimal salary { get; set; }

        public User(UserModel userModel)
        {
            email = userModel.email;
            password = userModel.password;
            user_name = userModel.user_name;
            cpf = userModel.cpf;
            birth_date = userModel.birth_date;
            salary = 0.00M;
        }
        public User() { }
    }
}
