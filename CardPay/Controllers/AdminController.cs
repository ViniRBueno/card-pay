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
        private readonly IFamilyService _familyService;

        public AdminController(IAdminService adminService, IFamilyService familyService)
        {
            _adminService = adminService;
            _familyService = familyService;
        }

        [HttpGet]
        [Route("loan/list")]
        [Authorize]
        public async Task<IActionResult> ListLoans([FromQuery]int? id)
        {
            if (!ValidateAdminToken())
                return Ok(BaseDTO<string>.Error("Login inválido para esta operação!"));

            var loans = _adminService.ListLoansByStatusId(id);

            return Ok(BaseDTO<List<ListLoanModel>>.Success("Loans retornados com sucesso!", loans));
        }

        [HttpGet]
        [Route("loan/detail/{id}")]
        [Authorize]
        public async Task<IActionResult> GetLoanDetail(int id)
        {
            if (!ValidateAdminToken())
                return Ok(BaseDTO<string>.Error("Login inválido para esta operação!"));

            var loanDetail = _adminService.GetLoanDetail(id);

            loanDetail.familyMembers = _familyService.GetFamilyMembers(loanDetail.family.id_user).ToList();

            return Ok(BaseDTO<LoanInfoModel>.Success("Loans retornados com sucesso!", loanDetail));
        }

        [HttpPatch]
        [Route("loan")]
        [Authorize]
        public async Task<IActionResult> UpdateLoan(UpdateLoanModel updateLoan)
        {
            if (!ValidateAdminToken())
                return Ok(BaseDTO<string>.Error("Login inválido para esta operação!"));

            var loan = _adminService.UpdateLoanStatus(updateLoan);

            return Ok(BaseDTO<Loan>.Success("Loan atualizado com sucesso!", loan));
        }


        [HttpPatch]
        [Route("parcel/{id}/{payed}")]
        [Authorize]
        public async Task<IActionResult> UpdateLoan(int id, bool payed)
        {
            if (!ValidateAdminToken())
                return Ok(BaseDTO<string>.Error("Login inválido para esta operação!"));

            var parcel = _adminService.UpdateParcelStatus(id, payed);

            return Ok(BaseDTO<Parcel>.Success("Parcela atualizada com sucesso!", parcel));
        }


        private bool ValidateAdminToken() => TokenManager.GetUser(User.Identity.Name).is_admin == true ? true : false;
    }
}
