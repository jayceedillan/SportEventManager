using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Features.SportCategories.Commands.CreateSportCategory;
using SportEventManager.Application.Features.SportCategories.Commands.DeleteSportCategory;
using SportEventManager.Application.Features.SportCategories.Commands.UpdateSportCategory;
using SportEventManager.Application.Features.SportCategories.Queries.GetAllSportCategories;
using SportEventManager.Application.Features.SportCategories.Queries.GetSportCategoryById;
using System.Net.Mime;

namespace SportEventManager.Controllers
{
    /// <summary>
    /// API endpoints for managing sport categories
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/sport-categories")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class SportCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SportCategoriesController> _logger;

        public SportCategoriesController(
            IMediator mediator,
            ILogger<SportCategoriesController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all sport categories with optional filtering and pagination
        /// </summary>
        /// <param name="filter">Optional filter parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of sport categories</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<SportCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(
            [FromQuery] PaginationFilterDto filter,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving sport categories with filter: {@Filter}", filter);

            var query = new GetAllSportCategoriesQuery(filter);
            var result = await _mediator.Send(query, cancellationToken);

            return result.TotalCount > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Retrieves a specific sport category by ID
        /// </summary>
        /// <param name="id">The ID of the sport category</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The requested sport category</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(SportCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ResponseCache(Duration = 60)] // Cache for 1 minute
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving sport category with ID: {Id}", id);

            var query = new GetSportCategoryByIdQuery(id);
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

        /// <summary>
        /// Creates a new sport category
        /// </summary>
        /// <param name="command">The creation command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The newly created sport category</returns>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(SportCategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
            [FromBody] CreateSportCategoryCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new sport category: {@Command}", command);

            var result = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id, version = "1.0" },
                result);
        }

        /// <summary>
        /// Updates an existing sport category
        /// </summary>
        /// <param name="id">The ID of the sport category to update</param>
        /// <param name="command">The update command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sport category</returns>
        [HttpPut("{id:int}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(SportCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateSportCategoryCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    { "Id", new[] { "The ID in the route must match the ID in the request body" } }
                }));
            }

            _logger.LogInformation("Updating sport category with ID {Id}: {@Command}", id, command);
  
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a sport category
        /// </summary>
        /// <param name="id">The ID of the sport category to delete</param>
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

            await _mediator.Send(new DeleteSportCategoryCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
