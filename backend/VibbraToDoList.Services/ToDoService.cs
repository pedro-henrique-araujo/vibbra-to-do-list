using VibbraToDoList.Data.Dto;
using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Services
{
    public interface ToDoService
    {
        Task CreateListToDosAsync(ToDoList toDoList);
        Task DeleteListChildrenAsync(ToDoList list);
        Task<ToDoList> PopulateToDosChildrenAsync(ToDoList list);
        Task UpdateListToDosAsync(UpdateToDoListDto toDoListDto);

        Task DeleteAsync(Guid id);
    }
}
