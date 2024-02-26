using ToDoList.Domain.Interfaces;
using TaskStatus = ToDoList.Domain.Enum.TaskStatus;

namespace ToDoList.Domain.Entity
{
    public class TaskStatusHistory : IEntity<long>
    {
        public long Id { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public long SingleTaskId { get; set; }
        public SingleTask SingleTask { get; set; }
    }
}
