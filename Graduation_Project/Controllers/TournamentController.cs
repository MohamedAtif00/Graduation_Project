using Graduation_Project.Application.CQRS.TournamentFeature.AddTournament;
using Graduation_Project.Application.CQRS.TournamentFeature.DeleteTournament;
using Graduation_Project.Application.CQRS.TournamentFeature.GetAllTournament;
using Graduation_Project.Application.CQRS.TournamentFeature.GetUsersInTournament;
using Graduation_Project.Application.CQRS.UserFeature.AddToTournament;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TournamentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<TournamentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllTournamentQuery());

            return Ok(result);
        }

        // GET api/<TournamentController>/5
        [HttpGet("GetAllUsersForTournament/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetUsersInTournamentCommand(id));

            return Ok(result);
        }

        // POST api/<TournamentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddTournamentCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        // POST api/<TournamentController>
        [HttpPost("AddUserToTournament")]
        public async Task<IActionResult> Post([FromBody] AddToTournamentCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        // PUT api/<TournamentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TournamentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteTournamentCommand(id));

            return Ok(result);
        }
    }
}
