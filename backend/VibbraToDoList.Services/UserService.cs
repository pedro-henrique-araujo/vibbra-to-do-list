using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Services
{
    public interface UserService
    {
        Task SignUpAsync(string userName);
        Task<User> GetByUserNameAsync(string userName);
        Task DeleteAsync(string userName);
    }
}