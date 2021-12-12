﻿using CardPay.Entities;
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

        public List<Loan> ListLoansByStatusId(int? id)
        {
            if (id == null)
                return _context.loans.ToList();

            return _context.loans.Where(l => l.id_loanstatus == id).ToList();
        }

        public LoanInfoModel GetLoanDetail(int id)
        {
            var loanInfo = new LoanInfoModel();
            loanInfo.loan = _context.loans.Where(l => l.id_loan == id).FirstOrDefault();
            loanInfo.family = _context.families.Where(f => f.id_family == loanInfo.loan.id_family).FirstOrDefault();
            loanInfo.familyMembers = _context.familyMembers.Where(m => m.id_family == loanInfo.family.id_family).ToList();
            return loanInfo;
        }

        public Loan UpdateLoanStatus(UpdateLoanModel updateLoan)
        {
            var loan = _context.loans.Where(l => l.id_loan == updateLoan.loanId).FirstOrDefault();

            if (loan == null)
                return null;

            if (!updateLoan.approved)
            {
                loan.id_loanstatus = (int)LoanStatusEnum.Rejected;
                loan.reason = updateLoan.reason;
            }
            else
            {
                loan.id_loanstatus = (int)LoanStatusEnum.Active;

                CreateParcels(loan);
            }

            UpdateRegister(loan);

            return loan;
        }

        public Parcel UpdateParcelStatus(int parcelId, bool paydOnTime)
        {
            var parcel = _context.parcels.Where(p => p.id_parcel == parcelId).FirstOrDefault();

            if (parcel == null)
                return null;

            parcel.id_status = paydOnTime ? 2:3;

            UpdateRegister(parcel);

            return parcel;
        }

        private void CreateParcels(Loan loan)
        {
            var parcelList = new List<Parcel>();
            var expire = DateTime.Now;
            for (int i = 1; i <= loan.total_parcels; i++)
            {
                expire.AddMonths(1);
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
            string vencimento = ExpireDays(expireDate).ToString().Replace("-","");
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
