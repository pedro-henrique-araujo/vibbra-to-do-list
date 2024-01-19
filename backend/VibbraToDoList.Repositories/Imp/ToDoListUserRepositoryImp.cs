using Microsoft.EntityFrameworkCore;
using VibbraToDoList.Data;
using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Repositories.Imp
{
    public class ToDoListUserRepositoryImp : ToDoListUserRepository
    {
        private readonly VibbraToDoListDbContext _dbContext;

        public ToDoListUserRepositoryImp(VibbraToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(ToDoListUser toDoListUser)
        {
            _dbContext.Entry(toDoListUser).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ToDoListUser?> GetByListIdAndUserIdAsync(ToDoListUser toDoListUser)
        {
            var listUser = await _dbContext
                .Set<ToDoListUser>()
                .FirstOrDefaultAsync(u => u.ToDoListId == toDoListUser.ToDoListId && u.UserId == toDoListUser.UserId);

            return listUser;
        }
    }
}
