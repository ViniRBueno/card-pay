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

        public CreateLoanResultModel CreateLoan(CreateLoanModel loanModel)
        {
            var loans = _context.loans.Where(l => l.id_family == loanModel.id_family).ToList();

            foreach (var loan in loans)
            {
                if (loan.id_loanstatus == (int)LoanStatusEnum.Active
                    || loan.id_loanstatus == (int)LoanStatusEnum.Created
                    || loan.id_loanstatus == (int)LoanStatusEnum.Rejected
                    || loan.create_date <= fiveYears)
                    return new CreateLoanResultModel() { error = "Só pode haver um empréstimo ativo por cadastro" };
            }

            var loanDb = new Loan(loanModel);

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
                result.loanEstimate = CreateEstimate(family);
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

        private LoanEstimateModel CreateEstimate(Family family)
        {
            var estimate = new LoanEstimateModel();
            var salary = family.total_salary;
            var totalAmount = (salary / 10) * 36;

            if (totalAmount > 10000.00M)
                estimate.loan_value = 10000.00M;
            else
                estimate.loan_value = totalAmount;

            estimate.fee = estimate.loan_value * 0.05M;
            estimate.total_parcels = 36;
            estimate.parcel_value = (estimate.loan_value + estimate.fee) / estimate.total_parcels;

            return estimate;
        }

        private Loan GetLoanByFamily(int familyId) => _context.loans.Where(l => l.id_family == familyId).FirstOrDefault();

        private Family GetFamily(int userId) => _context.families.Where(f => f.id_user == userId).FirstOrDefault();

        private IEnumerable<Parcel> GetParcelsByLoan(int loanId) => _context.parcels.Where(p => p.id_loan == loanId).ToList();
    }
}
