using CardPay.Entities;
using CardPay.Models;

namespace CardPay.Interfaces
{
    public interface IUserService
    {
        string ValidateExists(string cpf);
        string ValidateUser(UserModel user);
        string ValidateUser(UpdateUserModel user);
        int CreateUser(UserModel user);
        User GetUser(int id);
        bool UpdateAdditionalData(UpdateAdditionalUserModel userModel, int id);
        bool UpdateUser(UpdateUserModel userModel, int id);
        User UpdatePassword(PasswordModel password, int id);
        User ValidateUserLogin(LoginModel loginModel);
        Admin ValidateAdminLogin(LoginModel loginModel);
        string ValidatePassword(PasswordModel passwordModel, int userId);
        bool ValidateCPF(string cpf);
    }
}
