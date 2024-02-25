using ToDoList.Domain.Interfaces;

namespace ToDoList.Domain.Entity
{
    public class TaskList : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long UserId { get; set; }
        public User User { get; set; }
        public List<SingleTask> SingleTask { get; set;}

    }
}
