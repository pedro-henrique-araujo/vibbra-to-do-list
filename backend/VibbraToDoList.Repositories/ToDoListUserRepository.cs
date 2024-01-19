using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Repositories
{
    public interface ToDoListUserRepository
    {
        Task CreateAsync(ToDoListUser toDoListUser);
        Task<ToDoListUser?> GetByListIdAndUserIdAsync(ToDoListUser toDoListUser);
    }
}
