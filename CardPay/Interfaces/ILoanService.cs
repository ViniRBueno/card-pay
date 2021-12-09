using CardPay.Entities;
using CardPay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Interfaces
{
    public interface ILoanService
    {
        CreateLoanResultModel CreateLoan(CreateLoanModel loanModel);
        public LoanResultModel GetResultLoan(int id);
        public LoanWithParcelsModel GetLoanWithParcels(int id);
    }
}
