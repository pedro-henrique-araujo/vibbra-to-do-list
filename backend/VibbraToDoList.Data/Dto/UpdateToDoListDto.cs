using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Data.Dto
{
    public class UpdateToDoListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ToDo> ToDos { get; set; }
        public List<Guid> DeletedToDosIds { get; set; } = new();
    }
}
