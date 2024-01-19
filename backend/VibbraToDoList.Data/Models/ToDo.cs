namespace VibbraToDoList.Data.Models
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsDone { get; set; }
        public int? Index { get; set; }
        public Guid? ToDoListId { get; set; }
        public Guid? ToDoId { get; set; }
        public List<ToDo>? Children { get; set; }

        public void Initialize()
        {
            if (Id != Guid.Empty) return;
            Id = Guid.NewGuid();
        }
    }
}
