using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Services
{
    public interface ToDoListUserService
    {
        Task ShareAsync(ToDoListUser toDoListUser);
    }
}
