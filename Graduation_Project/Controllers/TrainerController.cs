using Graduation_Project.Application.CQRS.TrainerFeature.AddRating;
using Graduation_Project.Application.CQRS.TrainerFeature.AddTrainer;
using Graduation_Project.Application.CQRS.TrainerFeature.GetAllRating;
using Graduation_Project.Application.CQRS.TrainerFeature.GetAllTrainers;
using Graduation_Project.Application.CQRS.TrainerFeature.GetSingleTrainer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrainerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<TrainerController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllTrainersQuery());

            return Ok(result);
        }

        // GET api/<TrainerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetSingleTrainerQuery(id));
            return Ok(result);
        }

        // GET api/<TrainerController>/5
        [HttpGet("GetRatingsForTrainer/{id}")]
        public async Task<IActionResult> GetRates(Guid id)
        {
            var result = await _mediator.Send(new GetAllRatingQuery(id));
            return Ok(result);
        }


        // POST api/<TrainerController>
        [HttpPost("AddTrainer")]
        public async Task<IActionResult> Post([FromForm] AddTrainerCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        // POST api/<TrainerController>
        [HttpPost("RateTrainer")]
        public async Task<IActionResult> Post([FromBody] AddRatingCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        // PUT api/<TrainerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TrainerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
