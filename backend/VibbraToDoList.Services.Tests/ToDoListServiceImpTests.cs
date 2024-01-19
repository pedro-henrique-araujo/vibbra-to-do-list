using Moq;
using System.Collections.Generic;
using VibbraToDoList.Data.Dto;
using VibbraToDoList.Data.Models;
using VibbraToDoList.Repositories;
using VibbraToDoList.Services.Imp;

namespace VibbraToDoList.Services.Tests
{
    public class ToDoListServiceImpTests
    {
        private readonly Mock<ToDoListRepository> _toDoListRepositoryMock;
        private readonly Mock<ToDoListUserService> _toDoListUserServiceMock;
        private readonly Mock<ToDoService> _toDoServiceMock;
        private readonly ToDoListServiceImp _toDoListService;

        public ToDoListServiceImpTests()
        {
            _toDoListRepositoryMock = new Mock<ToDoListRepository>();
            _toDoListUserServiceMock = new Mock<ToDoListUserService>();
            _toDoServiceMock = new Mock<ToDoService>();
            _toDoListService = new ToDoListServiceImp(_toDoListRepositoryMock.Object, _toDoListUserServiceMock.Object, _toDoServiceMock.Object);
        }


        [Fact]
        public async Task GetPageAsync_WhenCalled_GetPageCorrectly()
        {
            var page = new PageDto<ToDoList>();
            var pageParamsDto = new ToDoListPageParamsDto();

            _toDoListRepositoryMock
                .Setup(r => r.GetPageAsync(pageParamsDto))
                .ReturnsAsync(page);

            var result = await _toDoListService.GetPageAsync(pageParamsDto);
            Assert.Equal(page, result);
        }

        [Fact]
        public async Task GetByIdAsync_WhenUserIsOwner_DoNotShare()
        {
            var userId = Guid.NewGuid();
            var populatetList = new ToDoList();

            var list = new ToDoList { OwnerUserId = userId };
            var paramsDto = new ToDoListParamsDto { UserId = userId };
            _toDoListRepositoryMock
                .Setup(r => r.GetByIdAsync(paramsDto.ToDoListId))
                .ReturnsAsync(list);
            _toDoServiceMock.Setup(s => s.PopulateToDosChildrenAsync(list))
               .ReturnsAsync(populatetList);

            var result = await _toDoListService.GetByIdAsync(paramsDto);
            Assert.Equal(populatetList, result);
            _toDoListUserServiceMock.Verify(s => s.ShareAsync(It.IsAny<ToDoListUser>()), Times.Never());
        }

        [Fact]
        public async Task GetByIdAsync_WhenUserIsNotOwner_Share()
        {
            var list = new ToDoList { OwnerUserId = Guid.NewGuid() };
            var populatetList = new ToDoList();
            var paramsDto = new ToDoListParamsDto { UserId = Guid.NewGuid() };
            _toDoListRepositoryMock
                .Setup(r => r.GetByIdAsync(paramsDto.ToDoListId))
                .ReturnsAsync(list);

            _toDoServiceMock.Setup(s => s.PopulateToDosChildrenAsync(list))
                .ReturnsAsync(populatetList);

            var result = await _toDoListService.GetByIdAsync(paramsDto);
            Assert.Equal(populatetList, result);
            _toDoListUserServiceMock.Verify(s => s.ShareAsync(It.IsAny<ToDoListUser>()));
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_CreateCorrectly()
        {
            var list = new ToDoList();
            await _toDoListService.CreateAsync(list);
            _toDoListRepositoryMock.Verify(r => r.CreateAsync(list));
            _toDoServiceMock.Verify(r => r.CreateListToDosAsync(list));
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_UpdateCorrectly()
        {
            var listInDb = new ToDoList();
            var idsToDelete = new List<Guid>();
            var listDto = new UpdateToDoListDto() { Id = Guid.NewGuid(), Name= "abc", DeletedToDosIds = idsToDelete};
            _toDoListRepositoryMock.Setup(r => r.GetByIdAsync(listDto.Id))
                .ReturnsAsync(listInDb);

            await _toDoListService.UpdateAsync(listDto);

            Assert.Equal(listDto.Name, listInDb.Name);
            _toDoListRepositoryMock.Verify(r => r.UpdateAsync(listInDb));
            _toDoServiceMock.Verify(r => r.UpdateListToDosAsync(listDto));
            Assert.All(idsToDelete, id => _toDoServiceMock.Verify(s => s.DeleteAsync(id)));

        }

        [Fact]
        public async Task DeleteAsync_WWhenUserIsOwner_DeleteCorrectly()
        {
            var userId = Guid.NewGuid();
            var list = new ToDoList() { OwnerUserId = userId };
            var paramsDto = new ToDoListParamsDto() { UserId = userId };
            _toDoListRepositoryMock.Setup(r => r.GetByIdAsync(paramsDto.ToDoListId))
                .ReturnsAsync(list);
            await _toDoListService.DeleteAsync(paramsDto);
            _toDoListRepositoryMock
                .Verify(r => r.DeleteAsync(list));


            _toDoServiceMock.Verify(r => r.DeleteListChildrenAsync(list));
        }

        [Fact] 
        public async Task DeleteAsync_WhenUserIsNotOwner_DoNotDelete()
        {
            var list = new ToDoList() { OwnerUserId = Guid.NewGuid()};
            var paramsDto = new ToDoListParamsDto() {  UserId = Guid.NewGuid()};
            _toDoListRepositoryMock.Setup(r => r.GetByIdAsync(paramsDto.ToDoListId))
                .ReturnsAsync(list);
            await _toDoListService.DeleteAsync(paramsDto);
            _toDoListRepositoryMock
                .Verify(r => r.DeleteAsync(list), Times.Never());
            _toDoServiceMock.Verify(r => r.DeleteListChildrenAsync(list), Times.Never());

        }
    }
}
