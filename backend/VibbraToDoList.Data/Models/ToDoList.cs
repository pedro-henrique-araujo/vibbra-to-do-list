namespace VibbraToDoList.Data.Models
{
    public class ToDoList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerUserId { get; set; }

        public List<ToDoListUser>? ToDoListsUsers { get; set; }
        public List<ToDo>? ToDos { get; set; }

        public void Initialize()
        {
            if (Id != Guid.Empty) return;
            Id = Guid.NewGuid();
        }
    }
}
