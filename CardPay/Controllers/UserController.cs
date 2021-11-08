using CardPay.Interfaces;
using CardPay.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CardPay.Controllers
{
    [Route("/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel user)
        {
            var validate = _userService.ValidateUser(user);

            if (string.IsNullOrEmpty(validate))
                return UnprocessableEntity(validate);

            _userService.CreateUser(user);

            return NoContent();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdatePassword([FromBody] NewPasswordModel passwordModel, [FromRoute] int id)
        {
            var user = _userService.GetUser(id);

            if (passwordModel == null)
                return NotFound();

            var update = _userService.UpdatePassword(passwordModel, id);

            if (!update)
                return BadRequest("Alguma das regras de nova senha não foi seguida, por favor, revise os dados enviados.");

            return NoContent();
        }

        [HttpGet]
        [Route("cpf/{cpf}")]
        public async Task<IActionResult> CPFValitation([FromRoute] string cpf)
        {
            var isValid = _userService.ValidateCPF(cpf);

            if (!isValid)
                return UnprocessableEntity("CPF Inválido!");

            var message = _userService.ValidateExists(cpf);

            return Ok(message);
        }

    }
}
