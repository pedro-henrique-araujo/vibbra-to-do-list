using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Repositories
{
    public interface UserRepository
    {
        Task CreateAsync(User user);
        Task<User> GetByUserNameAsync(string userName);
        Task DeleteAsync(User user);
    }
}