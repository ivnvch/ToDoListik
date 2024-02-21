using ToDoList.Domain.Interfaces;

namespace ToDoList.Domain.Entity
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<TaskList> TaskLists { get; set; } = new List<TaskList>();
        public ICollection<SingleTask> Tasks { get; set; } = new List<SingleTask>();
    }
}
