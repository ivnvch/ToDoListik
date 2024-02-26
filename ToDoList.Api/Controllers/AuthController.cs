using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Commands.LoginCommand;
using ToDoList.Application.Commands.RegisterCommand;
using ToDoList.Domain.Result;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseResult>> Register([FromBody] RegisterUserCommand registerUser,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(registerUser, cancellationToken);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResult>> Login([FromBody] LoginUserCommand loginUser,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(loginUser, cancellationToken);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
