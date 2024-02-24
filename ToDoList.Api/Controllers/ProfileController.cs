using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.Application.Commands.ProfileCommand;
using ToDoList.Application.Queries.GetUserQuery;
using ToDoList.Domain.Result;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<BaseResult>> GetPfofile() 
        {
             var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var getUserEmail = new GetUserForProfileQuery(email);
            var profile = await _mediator.Send(getUserEmail);
            if (profile is null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [HttpPut]
        public async Task<ActionResult<BaseResult>> UpdateProfile([FromBody] UpdateProfileCommand updateProfileCommand)
        {
            var profile = await _mediator.Send(updateProfileCommand);
            if (profile is null)
            {
                return NotFound(updateProfileCommand);
            }

            return Ok(profile);
        }
    }
}
