using VibbraToDoList.Data.Models;
using VibbraToDoList.Repositories;

namespace VibbraToDoList.Services.Imp
{
    public class UserServiceImp : UserService
    {
        private UserRepository _userRepository;

        public UserServiceImp(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);
            return user ?? new User();
        }

        public async Task SignUpAsync(string userName)
        {
            var userInDb = await _userRepository.GetByUserNameAsync(userName);
            if (userInDb is not null) return;
            var user = new User(userName);
            await _userRepository.CreateAsync(user);
        }

        public async Task DeleteAsync(string userName)
        {
            var userInDb = await _userRepository.GetByUserNameAsync(userName);
            if (userInDb is null) return;
            await _userRepository.DeleteAsync(userInDb);
        }
    }
}