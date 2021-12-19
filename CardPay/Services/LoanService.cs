using CardPay.Entities;
using CardPay.Enums;
using CardPay.Interfaces;
using CardPay.Lib;
using CardPay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardPay.Services
{
    public class LoanService : ILoanService
    {
        private DateTime fiveYears = DateTime.Now.AddYears(-5);
        CardPayContext _context;

        public LoanService()
        {
            var contextOptions = new DbContextOptionsBuilder<CardPayContext>()
                .UseSqlServer(@"Server=localhost;Database=DB_CARD_PAY;Integrated Security=True")
                .Options;

            _context = new CardPayContext(contextOptions);
        }

        public CreateLoanResultModel CreateLoan(decimal loanValue, int id)
        {
            var family = GetFamily(id);
            var loans = _context.loans.Where(l => l.id_family == family.id_family).ToList();

            foreach (var loan in loans)
            {
                if (loan.id_loanstatus == (int)LoanStatusEnum.Active
                    || loan.id_loanstatus == (int)LoanStatusEnum.Created
                    || loan.create_date <= fiveYears)
                    return new CreateLoanResultModel() { error = "Só pode haver um empréstimo ativo por cadastro" };
                
                if (loan.id_loanstatus == (int)LoanStatusEnum.Fraud)
                    return new CreateLoanResultModel() { error = "Seu empréstimo foi rejeitado por fraude, você não pode solicitar novos empréstimos" };
            }

            var loanDb = new Loan(loanValue, family.id_family);

            _context.loans.Add(loanDb);
            _context.SaveChanges();

            return new CreateLoanResultModel() { loanId = loanDb.id_loan, message = "Empréstimo criado com suceso!" };
        }

        public LoanWithParcelsModel GetLoanWithParcels(int id)
        {
            var fullLoan = new LoanWithParcelsModel();
            var family = GetFamily(id);
            fullLoan.loan = GetLoanByFamily(family.id_family);
            fullLoan.parcels = GetParcelsByLoan(fullLoan.loan.id_loan);
            return fullLoan;
        }

        public LoanResultModel GetResultLoan(int id)
        {
            var result = new LoanResultModel();
            var family = GetFamily(id);
            var loan = GetLoanByFamily(family.id_family);
            var amount = (family.total_salary / 10) * 36;
            if (loan == null)
            {
                result.loanStatusId = LoanResultEnum.Avaliable;
                result.loanEstimate = CreateEstimateValue(amount);
                return result;
            }

            if (loan.id_loanstatus == 1)
            {
                result.loanStatusId = LoanResultEnum.Waiting;
                result.statusDescription = "Aguardando Aprovação";
                return result;
            }

            if (loan.id_loanstatus == 2)
            {
                result.loanStatusId = LoanResultEnum.InProgress;
                result.loanModel = GetLoanWithParcels(id);
                return result;
            }

            if (loan.id_loanstatus == 3)
            {
                result.loanStatusId = LoanResultEnum.Negated;
                result.loanEstimate = CreateEstimateValue(amount);
                result.reason = loan.reason;
                return result;
            }

            if (loan.id_loanstatus == 4 && loan.create_date <= fiveYears)
            {
                result.loanStatusId = LoanResultEnum.Avaliable;
                result.loanEstimate = CreateEstimateValue(amount);
                return result;
            }

            if (loan.id_loanstatus == 5)
            {
                result.loanStatusId = LoanResultEnum.Fraud;
                result.statusDescription = "Negado análise de fraude";
                result.reason = "Negado análise de fraude";
                return result;
            }

            result.loanStatusId = LoanResultEnum.Finished;
            var nextLoan = loan.create_date.AddYears(5);
            result.statusDescription = $"Empréstimo finalizado, você poderá solicitar outro em: {nextLoan}";

            return result;
        }

        public List<Parcel> GetParcels(int id)
        {
            var family = GetFamily(id);
            var loan = _context.loans.Where(l => l.id_family == family.id_family && l.id_loanstatus == 2).FirstOrDefault();
            var parcels = _context.parcels.Where(p => p.id_loan == loan.id_loan).ToList();

            foreach (var parcel in parcels)
            {
                parcel.id_status = parcel.expire_date < DateTime.Now && parcel.id_status == 1 ? 3 : parcel.id_status;
            }

            return parcels;
        }

        public string GenerateParcelData(int userId, int parcelId, bool isAdmin = false)
        {
            var user = new User();
            var parcel = _context.parcels.Where(p => p.id_parcel == parcelId).FirstOrDefault();

            if (isAdmin)
            {
                var familyId = _context.loans.Where(l => l.id_loan == parcel.id_loan).FirstOrDefault().id_family;
                var idUser = _context.families.Where(f => f.id_family == familyId).FirstOrDefault().id_user;
                user = GetUserById(idUser);
            }
            else
            {
                user = GetUserById(userId);
            }
            
            if (ValidateGetParcel(user, parcel))
                return null;

            var boleto = new ZBoleto();
            boleto.NomeCliente = user.user_name;
            boleto.CpfCliente = user.cpf;
            boleto.NumerBoleto = parcel.id_parcel.ToString();
            boleto.Valor = (double)parcel.parcel_value;
            boleto.Descricao = $"Parcela número {parcel.parcel_number}";
            boleto.CodigoBarras = parcel.ticket_number;
            boleto.LinhaDigitavel = parcel.ticket_number.Insert(5,"-").Insert(11," ").Insert(18,".").Insert(25, " ").Insert(31, ".").Insert(38," ").Insert(40, " ");
            boleto.DataVencimento = parcel.expire_date;

            var geradorBoleto = new GeradorDeBoleto(boleto);
            return geradorBoleto.GerarBoleto();
        }

        private bool ValidateGetParcel(User user, Parcel parcel)
        {
            var family = GetFamily(user.id_user);
            var loan = _context.loans.Where(l => l.id_loan == parcel.id_loan).FirstOrDefault();

            if (loan.id_family == family.id_family)
                return false;
            return true;
        }

        public List<Bank> ListBanks() => _context.banks.ToList();


        public LoanEstimateModel CreateEstimateValue(decimal value)
        {
            var estimate = new LoanEstimateModel();

            if (value > 10000.00M)
                estimate.loan_value = 10000.00M;
            else
                estimate.loan_value = value;

            estimate.fee = estimate.loan_value * 0.05M;
            estimate.total_parcels = 36;
            estimate.parcel_value = Math.Round(((estimate.loan_value + estimate.fee) / estimate.total_parcels), 2);

            return estimate;

        }

        private Loan GetLoanByFamily(int familyId) => _context.loans.Where(l => l.id_family == familyId).OrderByDescending(l => l.id_loan).FirstOrDefault();
        
        private User GetUserById(int userId) => _context.users.Where(u => u.id_user == userId).FirstOrDefault();

        private Family GetFamily(int userId) => _context.families.Where(f => f.id_user == userId).FirstOrDefault();

        private IEnumerable<Parcel> GetParcelsByLoan(int loanId) => _context.parcels.Where(p => p.id_loan == loanId).ToList();
    }
}
