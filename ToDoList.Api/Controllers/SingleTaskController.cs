using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Commands.SingleTaskCommand.Create;
using ToDoList.Application.Commands.SingleTaskCommand.Delete;
using ToDoList.Application.Commands.SingleTaskCommand.Update;
using ToDoList.Application.Commands.TaskListCommand.Delete;
using ToDoList.Application.Commands.TaskListCommand.Update;
using ToDoList.Application.Dto.SingleTask;
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
        public async Task<ActionResult<BaseResult<CreateSingleTaskDto>>> CreateTaskList([FromBody] CreateSingleTaskCommand dto)
        {
            CreateSingleTaskCommand createTaskListCommand = new CreateSingleTaskCommand(dto.TaskListId,
                dto.Name, dto.Description, dto.DateCreated);//заменить

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
        public async Task<ActionResult<BaseResult<bool>>> DeleteTaskList([FromBody] DeleteSingleTaskCommand deleteSingleTaskCommand)
        {
            var response = await _mediator.Send(deleteSingleTaskCommand);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<UpdateSingleTaskDto>>> Update([FromBody] UpdateSingleTaskCommand updateSingleTaskCommand)
        {
            var response = await _mediator.Send(updateSingleTaskCommand);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
