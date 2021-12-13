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
        private readonly IUserService _userService;

        public FamilyController(IFamilyService familyService, IUserService userService)
        {
            _familyService = familyService;
            _userService = userService;
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
            try
            {
                var validate = ValidateMember(memberModel);
                if (validate != null)
                    return Ok(BaseDTO<string>.Error(validate));

                var userToken = TokenManager.GetUser(User.Identity.Name);
                var familyMember = _familyService.CreateFamilyMember(memberModel, userToken.id);
                return Ok(BaseDTO<FamilyMember>.Success("Memebro criado com sucesso", familyMember));
            }
            catch (Exception ex)
            {
                return Ok(BaseDTO<string>.Error(ex.Message));
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateMember([FromBody] FamilyMemberModel memberModel)
        {
            try
            {
                var validate = ValidateMember(memberModel);
                if (validate != null)
                    return Ok(BaseDTO<string>.Error(validate));

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
                return Ok(BaseDTO<string>.Success("Membro deletado com sucesso", ""));
            }
            catch (Exception ex)
            {
                return Ok(BaseDTO<string>.Error(ex.Message));
            }
        }

        private string ValidateMember(FamilyMemberModel familyMemberModel)
        {
            if (!_userService.ValidateCPF(familyMemberModel.cpf))
                return "CPF Inválido";

            var exists = _userService.ValidateExists(familyMemberModel.cpf);

            if (exists != null && familyMemberModel.id == 0)
                return exists;

            return null;
        }
    }
}
