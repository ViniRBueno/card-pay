﻿using CardPay.Interfaces;
using CardPay.Lib;
using CardPay.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [Route("{id}/create")]
        public async Task<IActionResult> CreateMember([FromBody] CreateLoanModel loanModel)
        {
            var loan = _loanService.CreateLoan(loanModel);

            if (loan.error != null)
                return UnprocessableEntity(loan.error);

            return Ok(loan);
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
