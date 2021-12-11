using CardPay.Entities;
using CardPay.Enums;
using CardPay.Interfaces;
using CardPay.Jwt;
using CardPay.Lib;
using CardPay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace CardPay.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        readonly IGeneratePdf _generatePdf;
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService, IGeneratePdf generatePdf)
        {
            _loanService = loanService;
            _generatePdf = generatePdf;
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> CreateLoan(decimal loanValue)
        {
            var userToken = TokenManager.GetUser(User.Identity.Name);
            var loan = _loanService.CreateLoan(loanValue, userToken.id);

            if (loan.error != null)
                return Ok(BaseDTO<string>.Error(loan.error));

            return Ok(BaseDTO<CreateLoanResultModel>.Success("Empréstimo criado com sucesso!", loan));
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> GetLoanLoad()
        {
            var userToken = TokenManager.GetUser(User.Identity.Name);
            var loan = _loanService.GetResultLoan(userToken.id);

            return Ok(BaseDTO<LoanResultModel>.Success("", loan));
        }


        [HttpGet]
        [Route("{value}")]
        [Authorize]
        public async Task<IActionResult> SimulateLoan(decimal value)
        {
            var estimate = _loanService.CreateEstimateValue(value);

            return Ok(BaseDTO<LoanEstimateModel>.Success("", estimate));
        }

        [HttpGet]
        [Route("banks")]
        [Authorize]
        public async Task<IActionResult> ListBanks()
        {
            var banks = _loanService.ListBanks();

            return Ok(BaseDTO<List<Bank>>.Success("Bancos retornados com sucesso!", banks));
        }

        [HttpGet]
        [Route("/boleto")]
        public async Task<IActionResult> GetBoleto()
        {
            var boleto = new ZBoleto();
            boleto.NomeCliente = "Judeu Santos";
            boleto.CpfCliente = "488.458.465-55";
            boleto.NumerBoleto = "";
            boleto.Valor = 1000;
            boleto.Descricao = "Parcela número 5";
            boleto.CodigoBarras = "23791690400000141501234090000000045301234560";
            boleto.LinhaDigitavel = "23791.23405 90000.000043 53012.345608 1 69040000014150";
            boleto.DataVencimento = DateTime.Now.AddMonths(1);


            var geradorBoleto = new GeradorDeBoleto(boleto);
            var htmlBoleto = geradorBoleto.GerarBoleto();
            var pdf = _generatePdf.GetPDF(htmlBoleto);
            var pdfStream = new System.IO.MemoryStream();
            pdfStream.Write(pdf, 0, pdf.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }
    }
}
