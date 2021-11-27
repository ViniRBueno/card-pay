using CardPay.Entities;
using CardPay.Interfaces;
using CardPay.Jwt;
using CardPay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardPay.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _familyService;

        public FamilyController(IFamilyService familyService)
        {
            _familyService = familyService;
        }

        [HttpGet]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> GetFamilyMembers()
        {
            var userToken = TokenManager.GetUser(User.Identity.Name);
            var members = _familyService.GetFamilyMembers(userToken.id);
            return Ok(BaseDTO<IEnumerable<FamilyMember>>.Success("Família encontrada com sucesso!", members));
        }

        [HttpPost]
        [Authorize]
        [Route("create-member")]
        public async Task<IActionResult> AddMember([FromBody] FamilyMemberModel memberModel)
        {
            var userToken = TokenManager.GetUser(User.Identity.Name);
            var familyMember = _familyService.CreateFamilyMember(memberModel, userToken.id);
            return Ok(BaseDTO<FamilyMember>.Success("Memebro criado com sucesso", familyMember));
        }
    }
}
