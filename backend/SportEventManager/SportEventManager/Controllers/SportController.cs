using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Features.SportCategories.DTOs;
using SportEventManager.Application.Features.Sports.Commands.CreateSport;
using SportEventManager.Application.Features.Sports.Commands.DeleteSport;
using SportEventManager.Application.Features.Sports.Commands.UpdateSport;
using SportEventManager.Application.Features.Sports.Queries.GetAllSport;
using SportEventManager.Application.Features.Sports.Queries.GetSportById;
using System.Net.Mime;

namespace SportEventManager.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/sport")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class SportController : ControllerBase
    {
        
        private readonly IMediator _mediator;
        private readonly ILogger<SportController> _logger;

        public SportController(
            IMediator mediator,
            ILogger<SportController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(SportDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ResponseCache(Duration = 60)] // Cache for 1 minute
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving sport category with ID: {Id}", id);

            var query = new GetSportByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            return result is not null
                ? Ok(result)
                : NotFound(new ProblemDetails
                {
                    Title = "Resource Not Found",
                    Detail = $"Sport with ID {id} was not found",
                    Status = StatusCodes.Status404NotFound
                });
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(SportDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
            [FromBody] CreateSportCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new sport: {@Command}", command);

            var result = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id, version = "1.0" },
                result);
        }

        /// <summary>
        /// Retrieves all sport  with optional filtering and pagination
        /// </summary>
        /// <param name="filter">Optional filter parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of sport </returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<SportDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(
            [FromQuery] PaginationFilterDto filter,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving sport with filter: {@Filter}", filter);

            var query = new GetAllSportQuery(filter);
            var result = await _mediator.Send(query, cancellationToken);

            return result.TotalCount > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Deletes a sport
        /// </summary>
        /// <param name="id">The ID of the sport to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>No content</returns>
        [HttpDelete("{id:int}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting sport category with ID: {Id}", id);

            await _mediator.Send(new DeleteSportCommand(id), cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing sport 
        /// </summary>
        /// <param name="id">The ID of the sport  to update</param>
        /// <param name="command">The update command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sport </returns>
        [HttpPut("{id:int}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(SportDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateSportCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "Id", new[] { "The ID in the route must match the ID in the request body" } }
                }));
            }

            _logger.LogInformation("Updating sport with ID {Id}: {@Command}", id, command);

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
