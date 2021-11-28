using CardPay.Entities;
using CardPay.Enums;
using CardPay.Interfaces;
using CardPay.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CardPay.Services
{
    public class LoanService : ILoanService
    {
        CardPayContext _context;
        public LoanService()
        {
            var contextOptions = new DbContextOptionsBuilder<CardPayContext>()
                .UseSqlServer(@"Server=localhost;Database=DB_CARD_PAY;Integrated Security=True")
                .Options;

            _context = new CardPayContext(contextOptions);
        }

        public LoanResultModel CreateLoan(CreateLoanModel loanModel)
        {
            var loans = _context.loans.Where(l => l.id_family == loanModel.id_family).ToList();
            foreach (var loan in loans)
            {
                if (loan.id_loanstatus == (int)LoanStatusEnum.Active
                    || loan.id_loanstatus == (int)LoanStatusEnum.Created)
                    return new LoanResultModel() { error = "Só pode haver um empréstimo ativo por cadastro" };
            }

            var loanDb = new Loan(loanModel);

            _context.loans.Add(loanDb);
            _context.SaveChanges();

            return new LoanResultModel() { loanId = loanDb.id_loan, message = "Empréstimo criado com suceso!" };
        }
    }
}
