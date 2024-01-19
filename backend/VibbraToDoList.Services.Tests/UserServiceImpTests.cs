using Moq;
using VibbraToDoList.Data.Models;
using VibbraToDoList.Repositories;
using VibbraToDoList.Services.Imp;

namespace VibbraToDoList.Services.Tests
{
    public class UserServiceImpTests
    {
        private readonly Mock<UserRepository> _userRepositoryMock;
        private readonly UserServiceImp _userService;

        public UserServiceImpTests()
        {
            _userRepositoryMock = new Mock<UserRepository>();
            _userService = new UserServiceImp(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task SignUpAsync_WhenUserDoesNotExist_CreateUser()
        {
             await _userService.SignUpAsync("a");
            _userRepositoryMock
                .Verify(u => u.CreateAsync(It.Is<User>(u => u.Id != Guid.Empty && u.UserName == "a")));
        }

        [Fact]
        public async Task SignUpAsync_WhenExists_DoNotCreateUser()
        {
            var user = new User("a");
            _userRepositoryMock.Setup(r => r.GetByUserNameAsync("a"))
                .ReturnsAsync(user);
            await _userService.SignUpAsync("a");
            _userRepositoryMock
                .Verify(u => u.CreateAsync(It.IsAny<User>()), Times.Never());
        }


        [Fact]
        public async Task GetByUserNameAsync_WhenUserExist_ReturnUser()
        {
            var user = new User("a");

            _userRepositoryMock.Setup(r => r.GetByUserNameAsync("a"))
                .ReturnsAsync(user);

            var result = await _userService.GetByUserNameAsync("a");

            Assert.Equal(result, user);
        }

        [Fact]
        public async Task GetByUserNameAsync_WhenUserDoesNotExist_ReturnEmptyUser()
        {
            var result = await _userService.GetByUserNameAsync("a");

            Assert.Equal(Guid.Empty, result.Id);
            Assert.Null(result.UserName);
        }

        [Fact]
        public async Task DeleteAsync_UserExists_DeleteUser()
        {
            var user = new User("a");

            _userRepositoryMock.Setup(r => r.GetByUserNameAsync("a"))
                .ReturnsAsync(user);
            await _userService.DeleteAsync("a");
            _userRepositoryMock.Verify(r => r.DeleteAsync(user));
        }

        [Fact]
        public async Task DeleteAsync_UserDoesNotExist_DoNotCallDelete()
        {
            await _userService.DeleteAsync("a");
            _userRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<User>()), Times.Never());
        }
    }
}