using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    class User
    {
        public int id_user { get; set; }
        public string user_name { get; set; }
        public string cpf { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
