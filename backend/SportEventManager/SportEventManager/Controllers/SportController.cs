using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportEventManager.Application.Features.Sports.Commands.CreateSport;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<ActionResult<IReadOnlyList<SportCategory>>> GetAll()
        //{
        //    var result = await _mediator.Send(new GetAllSportCategoriesQuery());
        //    return Ok(result);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<SportCategory>> GetById(int id)
        //{
        //    var result = await _mediator.Send(new GetSportCategoryByIdQuery { Id = id });
        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<ActionResult<Sport>> Create([FromBody] CreateSportCommand command)
        {
            var result = await _mediator.Send(command);

            return null;
            //return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<SportCategory>> Update(int id, [FromBody] UpdateSportCategoryCommand command)
        //{
        //    if (id != command.Id)
        //        return BadRequest();

        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await _mediator.Send(new DeleteSportCategoryCommand { Id = id });
        //    return NoContent();
        //}
    }
}
