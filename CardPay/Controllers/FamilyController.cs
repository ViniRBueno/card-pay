using CardPay.Interfaces;
using CardPay.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CardPay.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _familyService;

        public FamilyController(IFamilyService familyService)
        {
            _familyService = familyService;
        }

        [HttpPost]
        [Route("{id}/create-member")]
        public async Task<IActionResult> CreateMember([FromBody] FamilyMemberModel memberModel, int id)
        {
            _familyService.CreateFamilyMember(memberModel, id);

            return Ok(new { message = "Membro adicionado com sucesso" });
        }
    }
}
