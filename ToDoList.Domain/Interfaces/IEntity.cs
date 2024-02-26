namespace ToDoList.Domain.Interfaces
{
    public interface IEntity<in T> where T : struct
    {
        public long Id { get; set; }
    }
}
