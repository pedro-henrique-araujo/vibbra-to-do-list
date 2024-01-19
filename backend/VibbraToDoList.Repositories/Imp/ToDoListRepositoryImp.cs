using Microsoft.EntityFrameworkCore;
using VibbraToDoList.Data;
using VibbraToDoList.Data.Dto;
using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Repositories.Imp
{
    public class ToDoListRepositoryImp : ToDoListRepository
    {
        private readonly VibbraToDoListDbContext _dbContext;

        public ToDoListRepositoryImp(VibbraToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(ToDoList toDoList)
        {
            _dbContext.Entry(toDoList).State = EntityState.Added;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ToDoList toDoList)
        {
            _dbContext.Entry(toDoList).State = EntityState.Deleted;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ToDoList> GetByIdAsync(Guid id)
        {
            var list = await _dbContext.Set<ToDoList>()
                .Include(l => l.ToDos)
                .FirstOrDefaultAsync(l => l.Id == id);

            return list;
        }

        public async Task<PageDto<ToDoList>> GetPageAsync(ToDoListPageParamsDto pageParamsDto)
        {
            var page = new PageDto<ToDoList>();
            var queryable = _dbContext.Set<ToDoList>().Where(l => l.OwnerUserId == pageParamsDto.UserId
                || l.ToDoListsUsers.Any(u => u.UserId == pageParamsDto.UserId));

            page.Total = queryable.Count();
            page.Items = await queryable                
                .Skip(pageParamsDto.Skip)
                .Take(pageParamsDto.Take).ToListAsync();

            return page;
        }

        public async Task UpdateAsync(ToDoList toDoList)
        {
            var entry = _dbContext.Entry(toDoList); 
            entry.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            entry.State = EntityState.Detached;
        }
    }
}
