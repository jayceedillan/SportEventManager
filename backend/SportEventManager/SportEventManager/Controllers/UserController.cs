using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Features.Users.Commands.CreateUser;
using SportEventManager.Application.Features.Users.Commands.DeleteUser;
using SportEventManager.Application.Features.Users.Commands.UpdateUser;
using SportEventManager.Application.Features.Users.Queries.GetAllUser;
using SportEventManager.Application.Features.Users.Queries.GetUserByIdQuery;
using SportEventManager.Domain.Entities;
using System.Net.Mime;

namespace SportEventManager.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<User> _userManager;

        public UserController(IMediator mediator, ILogger<UserController> logger, UserManager<User> userManager)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")] // Only Admins can view user details
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching user with ID: {Id}", id);

            var result = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);

            return result is not null ? Ok(result) : NotFound(new { message = "User not found" });
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        [HttpPost]
        //[Authorize(Roles = "Admin")] // Only Admins can create users
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new user: {@Command}", command);

            var result = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = result.Id, version = "1.0" }, result);
        }

        /// <summary>
        /// Get all users with pagination and filters
        /// </summary>
        [HttpGet]
        //[Authorize(Roles = "Admin")] // Only Admins can view all users
        [ProducesResponseType(typeof(PaginatedResult<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilterDto filter, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching users with filter: {@Filter}", filter);

            var result = await _mediator.Send(new GetAllUserQuery(filter), cancellationToken);

            return result.TotalCount > 0 ? Ok(result) : NoContent();
        }

        /// <summary>
        /// Update a user
        /// </summary>
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest(new { message = "ID in route must match request body" });
            }

            _logger.LogInformation("Updating user with ID {Id}: {@Command}", id, command);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")] // Only Admins can delete users
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting user with ID: {Id}", id);

            await _mediator.Send(new DeleteUserCommand(id), cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Assign roles to a user
        /// </summary>
        [HttpPost("{id}/roles")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AssignRoles(string id, [FromBody] List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var result = await _userManager.AddToRolesAsync(user, roles);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Roles assigned successfully" });
        }
    }
}
