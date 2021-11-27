using CardPay.Entities;
using CardPay.Interfaces;
using CardPay.Jwt;
using CardPay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CardPay.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFamilyService _familyService;

        public UserController(IUserService userService, IFamilyService familyService)
        {
            _userService = userService;
            _familyService = familyService;
        }

        [HttpGet]
        [Route("token")]
        public async Task<IActionResult> Token()
        {
            User user = new User();
            user.email = "administrador@sistema.com";
            user.id_user = 1;
            var token = TokenManager.GenerateToken(user, 10);
            var cookieOptions = new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(1),
                IsEssential = true
            };
            Response.Cookies.Append("access-token", token, cookieOptions);
            return Ok(BaseDTO<string>.Success("Usuário criado com sucesso", token));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var user = _userService.ValidateLogin(loginModel);
                var token = TokenManager.GenerateToken(user, 10);
                var cookieOptions = new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(30),
                    IsEssential = true
                };
                Response.Cookies.Append("access-token", token, cookieOptions);
                return Ok(BaseDTO<string>.Success("Login feito com sucesso!", token));

            }
            catch (Exception ex)
            {
                return Ok(BaseDTO<string>.Error(ex.Message));
            }
        }

        [HttpGet]
        [Authorize]
        [Route("protected")]
        public async Task<IActionResult> Protected()
        {
            var user = TokenManager.GetUser(User.Identity.Name);
            return Ok("Olá " + user.user_name);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel user)
        {
            var validate = _userService.ValidateUser(user);

            if (!string.IsNullOrEmpty(validate))
                return Ok(BaseDTO<string>.Error(validate));

            var exists = _userService.ValidateExists(user.cpf);

            if (!string.IsNullOrEmpty(exists))
                return Ok(BaseDTO<string>.Error(exists));

            var id = _userService.CreateUser(user);

            _familyService.CreateFamily(id);

            return Ok(BaseDTO<int>.Success("Usuário criado com sucesso", id));
        }

        [HttpPut]
        [Authorize]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateAdditionalData userModel)
        {
            try
            {
                var userToken = TokenManager.GetUser(User.Identity.Name);
                var update = _userService.UpdateAdditionalData(userModel, userToken.id);
                _familyService.UpdateTotalSalary(userToken.id);

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = $"Um erro ocorreu durante a atualização dos seus dados. \n {ex.Message}" });
            }
        }

        [HttpPatch]
        [Authorize]
        [Route("update-additional-data")]
        public async Task<IActionResult> UpdateAdditionalData([FromBody] UpdateAdditionalData additionalData)
        {
            try
            {
                var userToken = TokenManager.GetUser(User.Identity.Name);
                var update = _userService.UpdateAdditionalData(additionalData, userToken.id);
                _familyService.UpdateTotalSalary(userToken.id);

                return Ok(BaseDTO<string>.Success("Dados adicionados com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Ok(BaseDTO<string>.Error($"Um erro ocorreu durante a atualização dos seus dados. \n {ex.Message}"));
            }
        }


        [HttpGet]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> GetUser()
        {
            var userToken = TokenManager.GetUser(User.Identity.Name);
            var user = _userService.GetUser(userToken.id);

            if (user == null)
                return Ok(BaseDTO<string>.Error("Usuário não encontrado"));

            return Ok(BaseDTO<User>.Success("Usuário Encontrado", user));
        }
    }
}
