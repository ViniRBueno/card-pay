using CardPay.Entities;
using CardPay.Models;

namespace CardPay.Interfaces
{
    public interface IUserService
    {
        string ValidateExists(string cpf);
        string ValidateUser(UserModel user);
        int CreateUser(UserModel user);
        User GetUser(int id);
        bool UpdateAdditionalData(UpdateAdditionalData userModel, int id);
    }
}
