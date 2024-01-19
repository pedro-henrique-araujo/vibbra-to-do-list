using Moq;
using VibbraToDoList.Data.Models;
using VibbraToDoList.Repositories;
using VibbraToDoList.Services.Imp;

namespace VibbraToDoList.Services.Tests
{
    public class ToDoListUserServiceImpTests
    {
        private readonly Mock<ToDoListUserRepository> _toDoListUserRepository;
        private readonly ToDoListUserServiceImp _toDoListUserService;

        public ToDoListUserServiceImpTests()
        {
            _toDoListUserRepository = new Mock<ToDoListUserRepository>();
            _toDoListUserService = new ToDoListUserServiceImp(_toDoListUserRepository.Object);
        }

        [Fact]
        public async Task ShareAsync_WhenListUserDoesNotExist_CallCreate()
        {
            var toDoListUser = new ToDoListUser();
            await _toDoListUserService.ShareAsync(toDoListUser);
            _toDoListUserRepository.Verify(r => r.CreateAsync(toDoListUser));
        }
        
        [Fact]
        public async Task ShareAsync_WhenListUserExist_DoNotCallCreate()
        {
            var toDoListUser = new ToDoListUser();
            _toDoListUserRepository.Setup(r => r.GetByListIdAndUserIdAsync(toDoListUser))
                .ReturnsAsync(new ToDoListUser());
            await _toDoListUserService.ShareAsync(toDoListUser);
            _toDoListUserRepository.Verify(r => r.CreateAsync(toDoListUser), Times.Never());
        }
    }
}
