using CardPay.Entities;
using CardPay.Enums;
using CardPay.Interfaces;
using CardPay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Services
{
    public class AdminService : IAdminService
    {
        CardPayContext _context;
        public AdminService()
        {
            var contextOptions = new DbContextOptionsBuilder<CardPayContext>()
                .UseSqlServer(@"Server=localhost;Database=DB_CARD_PAY;Integrated Security=True")
                .Options;

            _context = new CardPayContext(contextOptions);
        }

        public List<ListLoanModel> ListLoansByStatusId(int? id)
        {
            var loanListModel = new List<ListLoanModel>();
            var loans = new List<Loan>();

            if (id == null)
                loans = _context.loans.ToList();
            else
                loans = _context.loans.Where(l => l.id_loanstatus == id).ToList();

            foreach (var loan in loans)
            {
                var family = _context.families.Where(f => f.id_family == loan.id_family).FirstOrDefault();
                var user = _context.users.Where(u => u.id_user == family.id_user).FirstOrDefault();
                var members = _context.familyMembers.Where(fm => fm.id_family == family.id_family).ToList();
                var statusLoan = _context.loanstatuses.Where(s => s.id_loanstatus == loan.id_loanstatus).FirstOrDefault();

                loanListModel.Add(new ListLoanModel()
                {
                    userId = user.id_user,
                    userName = user.user_name,
                    cpf = user.cpf,
                    statusName = statusLoan.name_status,
                    loan = loan,
                    familyMembers = members,
                    SLA = (DateTime.Now - loan.create_date).Days
                });

            }

            return loanListModel;
        }

        public LoanInfoModel GetLoanDetail(int id)
        {
            var loanInfo = new LoanInfoModel();
            loanInfo.loan = _context.loans.Where(l => l.id_loan == id).FirstOrDefault();
            loanInfo.family = _context.families.Where(f => f.id_family == loanInfo.loan.id_family).FirstOrDefault();
            loanInfo.parcels = _context.parcels.Where(p => p.id_loan == loanInfo.loan.id_loan).ToList();
            foreach (var parcel in loanInfo.parcels)
            {
                parcel.id_status = parcel.expire_date < DateTime.Now && parcel.id_status == 1 ? 3 : parcel.id_status;
            }

            return loanInfo;
        }

        public Loan UpdateLoanStatus(UpdateLoanModel updateLoan)
        {
            var loan = _context.loans.Where(l => l.id_loan == updateLoan.loanId).FirstOrDefault();

            if (loan == null)
                return null;

            if (updateLoan.approved == 2)
            {
                loan.id_loanstatus = (int)LoanStatusEnum.Rejected;
                loan.reason = updateLoan.reason;
            }
            else if (updateLoan.approved == 1)
            {
                loan.id_loanstatus = (int)LoanStatusEnum.Active;

                CreateParcels(loan);
            }
            else
            {
                loan.id_loanstatus = (int)LoanStatusEnum.Fraud;
            }

            UpdateRegister(loan);

            return loan;
        }

        public Parcel UpdateParcelStatus(int parcelId, bool payed)
        {
            var parcel = _context.parcels.Where(p => p.id_parcel == parcelId).FirstOrDefault();

            if (parcel == null)
                return null;

            parcel.id_status = payed ? 2 : 3;

            UpdateRegister(parcel);

            return parcel;
        }

        private void CreateParcels(Loan loan)
        {
            var parcelList = new List<Parcel>();
            ;
            for (int i = 1; i <= loan.total_parcels; i++)
            {
                var expire = DateTime.Now.AddMonths(i);
                parcelList.Add(new Parcel
                {
                    id_loan = loan.id_loan,
                    parcel_value = loan.parcel_value,
                    id_status = 1,
                    parcel_number = i,
                    ticket_number = CreateTicketNumber(loan, i, expire),
                    expire_date = expire
                });
            }

            foreach (var parcel in parcelList)
            {
                CreateRegister(parcel);
            }
        }

        private string CreateTicketNumber(Loan loan, int iterator, DateTime expireDate)
        {
            string chumbado = "1029";
            string misturador = loan.id_family.ToString() + loan.id_loan.ToString() + loan.parcel_amount.ToString() + iterator.ToString();
            misturador = AdicionaZeros(misturador, 25) + "1";
            string vencimento = ExpireDays(expireDate).ToString().Replace("-", "");
            string valorParcela = loan.parcel_value.ToString().Replace(",", "");
            valorParcela = AdicionaZeros(valorParcela, 10);
            return chumbado + misturador + vencimento + valorParcela;
        }

        private int ExpireDays(DateTime expire)
        {
            DateTime DataBancoCentral = new DateTime(1997, 10, 07);
            return (int)DataBancoCentral.Subtract(expire).TotalDays;
        }

        private string AdicionaZeros(string valor, int numZeros)
        {
            string zeros = "";
            var tamanho = valor.Length;
            for (int i = 0; i < numZeros - tamanho; i++)
            {
                zeros += "0";
            }
            var formatado = zeros + valor;
            return formatado;
        }

        private void CreateRegister<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        private void UpdateRegister<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
