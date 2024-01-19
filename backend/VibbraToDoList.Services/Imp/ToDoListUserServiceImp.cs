using VibbraToDoList.Data.Models;
using VibbraToDoList.Repositories;

namespace VibbraToDoList.Services.Imp
{
    public class ToDoListUserServiceImp : ToDoListUserService
    {
        private readonly ToDoListUserRepository _toDoListUserRepository;

        public ToDoListUserServiceImp(ToDoListUserRepository toDoListUserRepository)
        {
            _toDoListUserRepository = toDoListUserRepository;
        }

        public async Task ShareAsync(ToDoListUser toDoListUser)
        {
            var toDoListUserInDb = await _toDoListUserRepository.GetByListIdAndUserIdAsync(toDoListUser);
            if (toDoListUserInDb is not null) return;
            toDoListUser.Id = Guid.NewGuid();
            await _toDoListUserRepository.CreateAsync(toDoListUser);
        }
    }
}
