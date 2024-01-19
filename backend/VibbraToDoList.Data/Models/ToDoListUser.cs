namespace VibbraToDoList.Data.Models
{
    public class ToDoListUser
    {
        public Guid Id { get; set; }
        public Guid ToDoListId { get; set; }
        public Guid UserId { get; set; }
    }
}
