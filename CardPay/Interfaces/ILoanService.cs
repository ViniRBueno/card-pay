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
        CreateLoanResultModel CreateLoan(decimal loanValue, int id);
        public LoanResultModel GetResultLoan(int id);
        public LoanWithParcelsModel GetLoanWithParcels(int id);
        public LoanEstimateModel CreateEstimateValue(decimal value);
        List<Bank> ListBanks();
        List<Parcel> GetParcels(int id);
        string GenerateParcelData(int userId, int parcelId, bool isAdmin);
    }
}
