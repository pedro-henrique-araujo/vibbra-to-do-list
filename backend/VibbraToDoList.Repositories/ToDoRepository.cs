using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Repositories
{
    public interface ToDoRepository
    {
        Task CreateAsync(ToDo toDo);
        Task DeleteAsync(ToDo toDo);
        Task<ToDo> GetByIdAsync(Guid id);
        Task<List<ToDo>?> GetByParentIdAsync(Guid parentId);
        void MarkAsDeleted(ToDo toDo);
        Task SaveChangesAsync();
        Task UpdateAsync(ToDo toDo);
    }
}
