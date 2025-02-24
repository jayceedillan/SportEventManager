using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Features.Players.Commands.CreatePlayer;
using SportEventManager.Application.Features.Players.Commands.DeletePlayer;
using SportEventManager.Application.Features.Players.Commands.UpdatePlayer;
using SportEventManager.Application.Features.Players.Queries.GetAllPlayer;
using SportEventManager.Application.Features.Players.Queries.GetPlayerByIdQuery;
using SportEventManager.Application.Features.Venues.Commands.DeleteEvent;
using SportEventManager.Application.Features.Venues.Commands.UpdateEvent;
using System.Net.Mime;

namespace SportEventManager.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/player")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class PlayerController : ControllerBase
    {
        
        private readonly IMediator _mediator;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(
            IMediator mediator,
            ILogger<PlayerController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ResponseCache(Duration = 60)] // Cache for 1 minute
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving sport category with ID: {Id}", id);

            var query = new GetPlayerByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            return result is not null
                ? Ok(result)
                : NotFound(new ProblemDetails
                {
                    Title = "Resource Not Found",
                    Detail = $"Sport category with ID {id} was not found",
                    Status = StatusCodes.Status404NotFound
                });
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
            [FromBody] CreatePlayerCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new Player: {@Command}", command);

            var result = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id, version = "1.0" },
                result);
        }

        /// <summary>
        /// Retrieves all Player  with optional filtering and pagination
        /// </summary>
        /// <param name="filter">Optional filter parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of Player </returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<PlayerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(
            [FromQuery] PaginationFilterDto filter,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving Player with filter: {@Filter}", filter);

            var query = new GetAllPlayerQuery(filter);
            var result = await _mediator.Send(query, cancellationToken);

            return result.TotalCount > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Deletes a Player
        /// </summary>
        /// <param name="id">The ID of the Player to delete</param>
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
            _logger.LogInformation("Deleting venue with ID: {Id}", id);

            await _mediator.Send(new DeletePlayerCommand(id), cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing Player 
        /// </summary>
        /// <param name="id">The ID of the Player  to update</param>
        /// <param name="command">The update command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated event </returns>
        [HttpPut("{id:int}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdatePlayerCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "Id", new[] { "The ID in the route must match the ID in the request body" } }
                }));
            }

            _logger.LogInformation("Updating Player with ID {Id}: {@Command}", id, command);

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
