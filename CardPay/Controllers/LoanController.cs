using CardPay.Interfaces;
using CardPay.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPay.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {

        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
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
    }
}
