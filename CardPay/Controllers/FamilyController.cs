using CardPay.Entities;
using CardPay.Interfaces;
using CardPay.Jwt;
using CardPay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> GetFamilyMembers()
        {
            var userToken = TokenManager.GetUser(User.Identity.Name);
            var members = _familyService.GetFamilyMembers(userToken.id);
            return Ok(BaseDTO<IEnumerable<FamilyMember>>.Success("Família encontrada com sucesso!", members));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddMember([FromBody] FamilyMemberModel memberModel)
        {
            var userToken = TokenManager.GetUser(User.Identity.Name);
            var familyMember = _familyService.CreateFamilyMember(memberModel, userToken.id);
            return Ok(BaseDTO<FamilyMember>.Success("Memebro criado com sucesso", familyMember));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateMember([FromBody] FamilyMemberModel memberModel)
        {
            try
            {
                var userToken = TokenManager.GetUser(User.Identity.Name);
                var familyMember = _familyService.UpdateFamilyMember(memberModel, userToken.id);
                return Ok(BaseDTO<FamilyMember>.Success("Memebro atualizado com sucesso", familyMember));
            }
            catch (Exception ex)
            {
                return Ok(BaseDTO<string>.Error(ex.Message));
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userToken = TokenManager.GetUser(User.Identity.Name);
                _familyService.DeleteFamilyMember(id, userToken.id);
                return Ok(BaseDTO<string>.Success("Memebro deletado com sucesso", ""));
            }
            catch (Exception ex)
            {
                return Ok(BaseDTO<string>.Error(ex.Message));
            }
        }
    }
}
