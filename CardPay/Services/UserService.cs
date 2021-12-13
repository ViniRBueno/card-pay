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

        public bool UpdateAdditionalData(UpdateAdditionalUserModel additionalData, int id)
        {
            try
            {
                var user = GetUser(id);
                //if (HasActiveLoanRequest(id))
                //    throw new System.Exception("Você não pode alterar esse tipo de informação pois já possui um empréstimo pendente de aprovação!");
                
                if (additionalData.salary.HasValue)
                {
                    user.salary = additionalData.salary.Value;
                    _context.users.Update(user);
                }
                if (additionalData.account != null)
                {
                    var account = new Account(additionalData.account, id);
                    var oldAccount = _context.accounts.Where(acc => acc.id_user == id && acc._active == 1).FirstOrDefault();

                    if (oldAccount != null)
                    {
                        oldAccount._active = 0;
                        _context.accounts.Update(oldAccount);
                    }

                    _context.accounts.Add(account);
                }
                _context.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }

        //public bool HasActiveLoanRequest(int id_user)
        //{
        //    var familly = _context.families.Where(f => f.id_user == id_user).FirstOrDefault();
        //    var lastLoan = _context.loans.Where(l => l.id_family == familly.id_family).OrderByDescending(l => l.id_loan).FirstOrDefault();
        //    return lastLoan.IsActive() || lastLoan.IsPendingApproval();
        //}

        public User UpdatePassword(PasswordModel password, int id)
        {
            var user = GetUser(id);
            user.password = password.newPassword;
            _context.users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public bool UpdateUser(UpdateUserModel userModel, int id)
        {
            var user = GetUser(id);

            user.birth_date = userModel.birth_date;
            user.user_name = userModel.user_name;
            user.email = userModel.email;
            user.cpf = userModel.cpf;

            _context.users.Update(user);
            _context.SaveChanges();

            return true;
        }

        public string ValidatePassword(PasswordModel passwordModel, int userId)
        {
            var user = GetUser(userId);
            if (user.password != passwordModel.oldPassword)
                return "A senha atual informada não confere com sua senha atual!";

            if (passwordModel.oldPassword == passwordModel.newPassword || passwordModel.newPassword == user.password)
                return "Sua senha atual não pode ser igual a sua senha antiga!";

            if (!ValidatePassword(passwordModel.newPassword))
                return "Sua senha deve ter ao menos um caractere maiúsculo, um minúsculo, e um caractere especial.";

            return null;
        }

        public string ValidateUser(UpdateUserModel user)
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
                return "E-mail inválido";

            if (user.email.Length < 11)
                return "Seu login deve ter menos de 11 caracteres";

            return null;
        }

        public User ValidateUserLogin(LoginModel loginModel)
        {
            if (ValidateLoginData(loginModel))
                throw new System.Exception(loginErr);

            var user = GetUserByLogin(loginModel.login);

            if (user == null)
                return null;

            if (user.password != loginModel.password)
                throw new System.Exception(loginErr);

            return user;
        }

        public Admin ValidateAdminLogin(LoginModel loginModel)
        {
            if (ValidateLoginData(loginModel))
                throw new System.Exception(loginErr);

            var admin = GetAdminByLogin(loginModel.login);

            if (admin == null)
                throw new System.Exception(loginErr);

            if (admin.password != loginModel.password)
                throw new System.Exception(loginErr);

            return admin;
        }

        public string ValidateUser(UserModel user)
        {
            if (!ValidateCPF(user.cpf))
                return "CPF Inválido";

            if (!ValidateEmail(user.email))
                return "E-mail Inválido!";

            if (!ValidatePassword(user.password))
                return "Sua senha deve ter ao menos um caractere maiúsculo, um minúsculo, e um caractere especial.";

            if (string.IsNullOrEmpty(user.user_name))
                return "Você precisa digitar um nome válido";

            if (user.user_name.Length > 100)
                return "Seu nome deve ter menos de 100 caracteres";

            if (string.IsNullOrEmpty(user.email))
                return "E-mail inválido";

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

        private Admin GetAdminByLogin(string login) => _context.admins.Where(a => a.email == login).FirstOrDefault();
        #endregion
    }
}
