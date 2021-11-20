using CardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Interfaces
{
    public interface ILoanService
    {
        LoanResultModel CreateLoan(CreateLoanModel loanModel);
    }
}
