using CardPay.Entities;
using CardPay.Enums;
using CardPay.Interfaces;
using CardPay.Models;
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
                    || loan.id_loanstatus == (int)LoanStatusEnum.Rejected
                    || loan.create_date <= fiveYears)
                    return new CreateLoanResultModel() { error = "Só pode haver um empréstimo ativo por cadastro" };
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

            if (loan == null)
            {
                result.loanStatusId = LoanResultEnum.Avaliable;
                var amount = (family.total_salary / 10) * 36;
                result.loanEstimate = CreateEstimateValue(amount);
                return result;
            }

            if (loan.id_loanstatus == (int)LoanStatusEnum.Rejected)
            {
                result.loanStatusId = LoanResultEnum.Negated;
                result.reason = loan.reason;
                return result;
            }

            if (loan.id_loanstatus == (int)LoanStatusEnum.Created)
            {
                result.loanStatusId = LoanResultEnum.Waiting;
                result.statusDescription = "Aguardando Aprovação";
                return result;
            }

            result.loanStatusId = LoanResultEnum.InProgress;
            result.loanModel = GetLoanWithParcels(id);

            return result;
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

        private Loan GetLoanByFamily(int familyId) => _context.loans.Where(l => l.id_family == familyId).FirstOrDefault();

        private Family GetFamily(int userId) => _context.families.Where(f => f.id_user == userId).FirstOrDefault();

        private IEnumerable<Parcel> GetParcelsByLoan(int loanId) => _context.parcels.Where(p => p.id_loan == loanId).ToList();
    }
}
