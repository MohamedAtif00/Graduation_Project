using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Graduation_Project.Application.Abstraction;
using Graduation_Project.Application.DTOs.Authentication;
using MediatR;
using Graduation_Project.Application.CQRS.UserFeature.AddUser;
using Graduation_Project.Domain.Entity.UserDomain;
using Graduation_Project.Application.CQRS.UserFeature.GetSingleUser;

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
        public async Task<IActionResult> Get(Guid id)
        {
            var result  = await _mediator.Send(new GetSingleUserQuery(id));

            return Ok(result);
        }


        // POST api/<AuthenticationController>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            var result = await _authenticationService.Register($"{request.firstName+request.lastName}", request.email, request.password, "User");

            if (result.Value != null && result.Errors.Count() == 0) await _mediator.Send(new AddUserCommand(result.Value.UserId,
                                                                                                            request.firstName,
                                                                                                            request.lastName,
                                                                                                            request.birthDate,
                                                                                                            request.nationalId,
                                                                                                            request.city,
                                                                                                            request.phone,
                                                                                                            request.image,
                                                                                                            request.gender,
                                                                                                            request.tennisCourt,
                                                                                                            request.TennisExp,
                                                                                                            new TimeSession(request.timeSession.Hours,request.timeSession.Minutes,request.timeSession.AmPm),
                                                                                                            request.trainerId,
                                                                                                            request.hasHealthCondition,
                                                                                                            request.detials));

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.Login(request.email, request.password, "User");

            return Ok(result);
        }

        [HttpPost("LoginAdmin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.Login(request.email, request.password, "Admin");

            return Ok(result);
        }
        // PUT api/<AuthenticationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpGet("AllowAccess/{token}")]
        public async Task<IActionResult> AllowAccess(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var claims = jsonToken.Claims;

            var claimIdenity = new ClaimsIdentity(jsonToken.Claims);
            var principle = new ClaimsPrincipal(claimIdenity);
            string userid = claims.FirstOrDefault(x => x.Type == "userid").Value;
            string username = claims.FirstOrDefault(x => x.Type == "username").Value;
            string email = claims.FirstOrDefault(x => x.Type == "email").Value;
            string role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

            var user = _userManager.FindByNameAsync(username);
            if (user == null) return NotFound("this user is not exist");


            var response = new AllowAccessResponse(userid, username, role, email, token);

            return Ok(response);
        }



    }
}
