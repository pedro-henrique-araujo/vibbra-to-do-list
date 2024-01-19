using VibbraToDoList.Data.Dto;
using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Services
{
    public interface ToDoListService
    {
        Task CreateAsync(ToDoList toDoList);
        Task DeleteAsync(ToDoListParamsDto toDoListParamsDto);
        Task<ToDoList> GetByIdAsync(ToDoListParamsDto paramsDto);
        Task<PageDto<ToDoList>> GetPageAsync(ToDoListPageParamsDto pageParamsDto);
        Task UpdateAsync(UpdateToDoListDto listDto);
    }
}