using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Application.DTOs.Authentication;
using MediatR;
using Graduation_Project.Application.CQRS.UserFeature.AddUser;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public AuthenticationController(IAuthenticationService authenticationService, IMediator mediator, UserManager<IdentityUser<Guid>> userManager)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
            _userManager = userManager;
        }

        // GET: api/<AuthenticationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthenticationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthenticationController>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authenticationService.Register(request.username, request.email, request.password, "User");

            if (result.Value != null && result.Errors.Count() == 0) await _mediator.Send(new AddUserCommand(result.Value.UserId,
                                                                                                            request.birthDate,
                                                                                                            request.nationalId,
                                                                                                            request.city,
                                                                                                            request.phone,
                                                                                                            request.image,
                                                                                                            request.gender,
                                                                                                            request.tennisCourt,
                                                                                                            request.trainerId,
                                                                                                            request.hasHealthCondition,
                                                                                                            request.detials));

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.Login(request.username, request.password, "User");

            return Ok(result);
        }

        [HttpPost("LoginAdmin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.Login(request.username, request.password, "Admin");

            return Ok(result);
        }
        // PUT api/<AuthenticationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthenticationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        

    }
}
