using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.Application.Commands.SingleTaskCommand.Create;
using ToDoList.Application.Commands.TaskListCommand.Create;
using ToDoList.Application.Dto.TaskList;
using ToDoList.Domain.Result;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SingleTaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SingleTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TaskListDto>>> CreateTaskList([FromBody] CreateSingleTaskCommand createSingleCommand)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            CreateSingleTaskCommand createTaskListCommand = new CreateSingleTaskCommand(createSingleCommand.id, createSingleCommand.Email,
                createSingleCommand.Name, createSingleCommand.Description, createSingleCommand.DateCreated, createSingleCommand.TaskStatus);//заменить

            var response = await _mediator.Send(createTaskListCommand);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
