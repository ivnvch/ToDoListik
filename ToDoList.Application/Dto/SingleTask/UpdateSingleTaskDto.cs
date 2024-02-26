namespace ToDoList.Application.Dto.SingleTask
{
    public record UpdateSingleTaskDto(long TaskId, long TaskListId, string Name, string Description, string Status);
}
