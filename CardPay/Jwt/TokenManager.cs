using CardPay.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Jwt
{
    public static class TokenManager
    {
        private static TokenJwtManager tokenInstance;
        private static string token = "Iss0Seri@UmT0kenSegur0";
        static TokenManager()
        {
            tokenInstance = new TokenJwtManager(token);
        }
        internal static string GetSecret()
        {
            return token;
        }
        public static string GenerateToken(User user, int minutes = 720)
        {
            string data = JsonConvert.SerializeObject(new UserToken(user));
            return tokenInstance.GenerateToken(data, minutes);
        }
        public static UserToken GetUser(string data)
        {
            UserToken user = JsonConvert.DeserializeObject<UserToken>(data);
            return user;
        }

    }
}
