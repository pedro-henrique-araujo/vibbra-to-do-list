using VibbraToDoList.Data.Dto;
using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Repositories
{
    public interface ToDoListRepository
    {
        Task CreateAsync(ToDoList toDoList);
        Task DeleteAsync(ToDoList toDoList);
        Task<ToDoList> GetByIdAsync(Guid id);
        Task<PageDto<ToDoList>> GetPageAsync(ToDoListPageParamsDto pageParamsDto);
        Task UpdateAsync(ToDoList toDoList);
    }
}
