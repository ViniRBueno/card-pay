using CardPay.Entities;
using CardPay.Interfaces;
using CardPay.Jwt;
using CardPay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [Route("list")]
        [Authorize]
        public async Task<IActionResult> ListLoans([FromQuery]int? id)
        {
            if (!ValidateAdminToken())
                return Ok(BaseDTO<string>.Error("Login inválido para esta operação!"));

            var loans = _adminService.ListLoansByStatusId(id);

            return Ok(BaseDTO<List<Loan>>.Success("Loans retornados com sucesso!", loans));
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetLoanDetail(int id)
        {
            if (!ValidateAdminToken())
                return Ok(BaseDTO<string>.Error("Login inválido para esta operação!"));

            var loan = _adminService.GetLoanDetail(id);

            return Ok(BaseDTO<LoanInfoModel>.Success("Loans retornados com sucesso!", loan));
        }

        [HttpPatch]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> UpdateLoan(UpdateLoanModel updateLoan)
        {
            if (!ValidateAdminToken())
                return Ok(BaseDTO<string>.Error("Login inválido para esta operação!"));

            var loan = _adminService.UpdateLoanStatus(updateLoan);

            return Ok(BaseDTO<Loan>.Success("Bancos retornados com sucesso!", loan));
        }

        private bool ValidateAdminToken() => TokenManager.GetUser(User.Identity.Name).is_admin == true ? true : false;
    }
}
