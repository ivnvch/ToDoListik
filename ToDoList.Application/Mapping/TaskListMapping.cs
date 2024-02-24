using AutoMapper;
using ToDoList.Application.Dto.TaskList;
using ToDoList.Domain.Entity;

namespace ToDoList.Application.Mapping
{
    public class TaskListMapping : Profile
    {
        public TaskListMapping()
        {
            CreateMap<TaskList, TaskListDto>().ReverseMap();
        }
    }
}
