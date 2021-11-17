using CardPay.Entities;
using CardPay.Models;

namespace CardPay.Interfaces
{
    public interface IUserService
    {
        bool ValidateCPF(string cpf);
        string ValidateExists(string cpf);
        string ValidateUser(UserModel user);
        int CreateUser(UserModel user);
        User GetUser(int id);
        bool UpdateUser(UpdateUserModel userModel, int id);
    }
}
