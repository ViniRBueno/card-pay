using CardPay.Entities;
using CardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Interfaces
{
    public interface IUserService
    {
        bool ValidateCPF(string cpf);
        string ValidateExists(string cpf);
        string ValidateUser(UserModel user);
        string CreateUser(UserModel user);
        User GetUser(int id);
        bool UpdatePassword(NewPasswordModel passwordModel, int id);
    }
}
