using Microsoft.EntityFrameworkCore;
using VibbraToDoList.Data;
using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Repositories.Imp
{
    public class UserRepositoryImp : UserRepository
    {
        private readonly VibbraToDoListDbContext _dbContext;

        public UserRepositoryImp(VibbraToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.UserName == userName);
            return user;
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Deleted; 
            await _dbContext.SaveChangesAsync();
        }

    }
}