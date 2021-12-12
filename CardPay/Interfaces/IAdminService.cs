using CardPay.Entities;
using CardPay.Models;
using System.Collections.Generic;

namespace CardPay.Interfaces
{
    public interface IAdminService
    {
        List<ListLoanModel> ListLoansByStatusId(int? id);
        Loan UpdateLoanStatus(UpdateLoanModel updateLoan);
        LoanInfoModel GetLoanDetail(int id);
        Parcel UpdateParcelStatus(int parcelId, bool paydOnTime);
    }
}
