using CardPay.Entities;
using CardPay.Interfaces;
using CardPay.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;

namespace CardPay.Services
{
    public class UserService : IUserService
    {
        const string loginErr = "Login ou senha inválidos";
        CardPayContext _context;
        public UserService()
        {
            var contextOptions = new DbContextOptionsBuilder<CardPayContext>()
                .UseSqlServer(@"Server=DESKTOP-GTMOM1F\SQLEXPRESS;Database=DB_CARD_PAY;Integrated Security=True;")
                .Options;

            _context = new CardPayContext(contextOptions);
        }

        public User GetUser(int id) => _context.users.Where(x => x.id_user == id).FirstOrDefault();

        public int CreateUser(UserModel userModel)
        {
            var user = new User(userModel);

            _context.users.Add(user);
            _context.SaveChanges();

            return user.id_user;
        }

        public bool UpdateAdditionalData(UpdateAdditionalData additionalData, int id)
        {
            try
            {
                var user = GetUser(id);
                user.salary = additionalData.salary;
                _context.users.Update(user);

                var account = new Account(additionalData.account, id);
                var oldAccount = _context.accounts.Where(acc => acc.id_user == id && acc._active == 0).FirstOrDefault();

                if (oldAccount != null)
                {
                    oldAccount._active = 0;
                    _context.accounts.Update(oldAccount);
                }

                _context.accounts.Add(account);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public User ValidateLogin(LoginModel loginModel)
        {
            if (ValidateLoginData(loginModel))
                throw new System.Exception(loginErr);

            var user = GetUserByLogin(loginModel.login);
            if (user == null)
                throw new System.Exception(loginErr);

            if (user.password != loginModel.password)
                throw new System.Exception(loginErr);

            return user;
        }

        public string ValidateUser(UserModel user)
        {
            if (!ValidateCPF(user.cpf))
                return "CPF Inválido";

            if (!ValidateEmail(user.email))
                return "E-mail Inválido!";

            if (!ValidatePassword(user.password))
                return "Senha inválida";

            if (string.IsNullOrEmpty(user.user_name))
                return "Você precisa digitar um nome válido";

            if (user.user_name.Length > 100)
                return "Seu nome deve ter menos de 100 caracteres";

            if (string.IsNullOrEmpty(user.email))
                return "Login inválido";

            if (user.email.Length < 11)
                return "Seu login deve ter menos de 11 caracteres";

            return null;
        }
        
        public string ValidateExists(string cpf)
        {
            if (_context.users.Where(user => user.cpf == cpf).FirstOrDefault() != null)
                return "CPF já cadastrado na base";

            return null;
        }

        #region Private Methods
        private bool ValidatePassword(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
            var isValidated = regex.IsMatch(password);

            if (string.IsNullOrEmpty(password) || !isValidated)
                return false;

            return true;
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

        private bool ValidateCPF(string cpf)
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

        private bool ValidateLoginData(LoginModel login) => string.IsNullOrEmpty(login.login) || string.IsNullOrEmpty(login.password) ? true : false;

        private User GetUserByLogin(string login) => _context.users.Where(u => u.email == login).FirstOrDefault();
        #endregion
    }
}
