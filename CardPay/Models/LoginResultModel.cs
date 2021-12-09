using CardPay.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Models
{
    public class LoginResultModel
    {
        public string token { get; set; }
        public UserToken user { get; set; }
    }
}
