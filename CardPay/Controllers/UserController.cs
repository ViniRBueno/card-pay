using CardPay.Interfaces;
using CardPay.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CardPay.Controllers
{
    [Route("/v1/[controller]")]
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

            if (!string.IsNullOrEmpty(validate))
                return UnprocessableEntity(new { message = validate } );

            var exists = _userService.ValidateExists(user.cpf);

            if (!string.IsNullOrEmpty(exists))
                return UnprocessableEntity(new { message = exists });

            var id = _userService.CreateUser(user);

            _familyService.CreateFamily(id);

            return Ok(new { message = $"Id: {id}" });
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel userModel, [FromRoute] int id)
        {
            try
            {
                var update = _userService.UpdateUser(userModel, id);
                _familyService.UpdateTotalSalary(id);

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = $"Um erro ocorreu durante a atualização dos seus dados. \n {ex.Message}" });
            }
        }
    }
}
