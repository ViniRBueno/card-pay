using CardPay.Entities;
using CardPay.Interfaces;
using CardPay.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace CardPay.Services
{
    public class UserService : IUserService
    {
        CardPayContext _context;
        public UserService()
        {
            var contextOptions = new DbContextOptionsBuilder<CardPayContext>()
                .UseSqlServer(@"Server=localhost;Database=DB_CARD_PAY;Integrated Security=True")
                .Options;

            _context = new CardPayContext(contextOptions);
        }

        public User GetUser(int id)
        {
            return _context.users.Where(x => x.id_user == id).FirstOrDefault() ?? new User();
        }

        public string CreateUser(UserModel userModel)
        {
            var user = new User().Convert(userModel);

            _context.users.Add(user);
            _context.SaveChanges();

            return user.id_user.ToString();
        }

        public bool UpdatePassword(NewPasswordModel passwordModel, int id)
        {

            var user = _context.users.Where(x => x.id_user == id).FirstOrDefault();

            var validate = ValidateUpdatePassword(passwordModel, user);

            if (!validate)
                return false;

            user.password = passwordModel.newPassword;
            _context.SaveChanges();

            return true;
        }

        public string ValidateUser(UserModel user)
        {
            if (!ValidateCPF(user.cpf))
                return "CPF Inválido";

            if (!ValidateEmail(user.email))
                return "E-mail Inválido!";

            if (string.IsNullOrEmpty(user.user_name))
                return "Você precisa digitar um nome válido";

            if (user.user_name.Length > 100)
                return "Seu nome deve ter menos de 100 caracteres";

            if (string.IsNullOrEmpty(user.email))
                return "Login inválido";

            if (user.email.Length < 11)
                return "Seu login deve ter menos de 11 caracteres";

            return ValidatePassword(user.password);
        }

        public bool ValidateCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public string ValidateExists(string cpf)
        {
            var exists = _context.users.Where(user => user.cpf == cpf).FirstOrDefault();
            if (exists != null)
                return "CPF já cadastrado na base";

            return null;
        }

        #region Private Methods
        private bool ValidateUpdatePassword(NewPasswordModel passwordModel, User user)
        {
            if (passwordModel.oldPassword != user.password)
                return false;

            if (ValidatePassword(passwordModel.newPassword) != null)
                return false;

            if (passwordModel.newPassword != passwordModel.confirmNewPassword)
                return false;

            return true;
        }
        private string ValidatePassword(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
            var isValidated = regex.IsMatch(password);

            if (string.IsNullOrEmpty(password))
                return "Senha inválida";

            if (!isValidated)
                return "Sua senha deve ter ao menos 8 caracteres, conter ao menos uma letra e um número.";

            return null;
        }
        private bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }
        #endregion
    }
}
