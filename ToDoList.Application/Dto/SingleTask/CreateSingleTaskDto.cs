namespace ToDoList.Application.Dto.SingleTask
{
    
    public record CreateSingleTaskDto(long TaskListId, string Name, string Description, DateTime DateCreated);
}
