using ToDoList.Domain.Interfaces;
using TaskStatus = ToDoList.Domain.Enum.TaskStatus;

namespace ToDoList.Domain.Entity
{
    public class SingleTask : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public TaskStatus Status { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public long TaskListId { get; set; }
        public TaskList TaskList { get; set; }
    }
}
