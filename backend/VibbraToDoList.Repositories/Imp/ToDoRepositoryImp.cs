using Microsoft.EntityFrameworkCore;
using VibbraToDoList.Data;
using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Repositories.Imp
{
    public class ToDoRepositoryImp : ToDoRepository
    {
        private readonly VibbraToDoListDbContext _dbContext;

        public ToDoRepositoryImp(VibbraToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(ToDo toDo)
        {
            _dbContext.Entry(toDo).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ToDo toDo)
        {
            _dbContext.Entry(toDo).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

        public void MarkAsDeleted(ToDo toDo)
        {
            _dbContext.Entry(toDo).State = EntityState.Deleted;
        }

        public async Task<ToDo> GetByIdAsync(Guid id)
        {
            var toDo = await _dbContext.Set<ToDo>().FirstOrDefaultAsync(t => t.Id == id);
            if (toDo is null) return toDo;
            _dbContext.Entry(toDo).State = EntityState.Detached;
            return toDo;
        }

        public async Task<List<ToDo>?> GetByParentIdAsync(Guid parentId)
        {
            var toDo = await _dbContext.Set<ToDo>()
                .AsNoTracking()
                .Where(t => t.ToDoId == parentId)
                .ToListAsync();
            return toDo;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDo toDo)
        {
            _dbContext.Entry(toDo).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
