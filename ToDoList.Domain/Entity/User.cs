using ToDoList.Domain.Interfaces;

namespace ToDoList.Domain.Entity
{
    public class User : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set;} = string.Empty;
        public List<TaskList> TaskList { get; set; }
        public UserToken Token { get; set; }
    }
}
