using CardPay.Entities;

namespace CardPay.Jwt
{
    public class UserToken
    {
        public UserToken() { } //não tira essa porra
        public UserToken(User user)
        {
            id = user.id_user;
            email = user.email;
            user_name = user.user_name;
            is_admin = false;
        }
        //public UserToken(Admin adm)
        //{
            //is_admin = true;
        //}
        public int id { get; set; }
        public string email { get; set; }
        public string user_name { get; set; }
        public bool is_admin { get; set; }
    }
}
