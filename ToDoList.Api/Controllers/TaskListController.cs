using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.Application.Commands.TaskListCommand.Create;
using ToDoList.Application.Commands.TaskListCommand.Delete;
using ToDoList.Application.Commands.TaskListCommand.Update;
using ToDoList.Application.Dto.TaskList;
using ToDoList.Domain.Result;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskListController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TaskListDto>>> CreateTaskList([FromBody] CreateTaskListCommand createTaskList)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            CreateTaskListCommand createTaskListCommand = new CreateTaskListCommand(createTaskList.Name, createTaskList.Description, email);//заменить

            var response = await _mediator.Send(createTaskListCommand);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<bool>>> DeleteTaskList([FromBody] DeleteTaskListCommand deleteTaskListCommand)
        {
            var response = await _mediator.Send(deleteTaskListCommand);
            
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TaskListDto>>> Update([FromBody] UpdateTaskListCommand updateTaskListCommand)
        {
            var response = await _mediator.Send(updateTaskListCommand);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
