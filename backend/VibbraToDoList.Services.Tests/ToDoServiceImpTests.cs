using Moq;
using VibbraToDoList.Data.Dto;
using VibbraToDoList.Data.Models;
using VibbraToDoList.Repositories;
using VibbraToDoList.Services.Imp;

namespace VibbraToDoList.Services.Tests
{
    public class ToDoServiceImpTests
    {
        private readonly Mock<ToDoRepository> _toDoRepositoryMock;
        private readonly ToDoServiceImp _toDoServiceImp;

        public ToDoServiceImpTests()
        {
            _toDoRepositoryMock = new Mock<ToDoRepository>();
            _toDoServiceImp = new ToDoServiceImp(_toDoRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateListToDosAsync_WhenToDosIsNull_DontCallCreateMethod()
        {
            var toDoList = new ToDoList { ToDos = null };
            await _toDoServiceImp.CreateListToDosAsync(toDoList);
            _toDoRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<ToDo>()), Times.Never());
        }

        [Fact]
        public async Task CreateListToDosAsync_WhenToDosIsNotNull_CallMethodsBasedOnItems()
        {
            var toDo1 = new ToDo();
            var toDo2 = new ToDo() { Children = new() { toDo1 } };
            var toDo3 = new ToDo() { Children = new() { toDo2 } };
            var toDoList = new ToDoList { ToDos = new() { toDo3 } };
            await _toDoServiceImp.CreateListToDosAsync(toDoList);
            _toDoRepositoryMock.Verify(r => r.CreateAsync(toDo1));
            _toDoRepositoryMock.Verify(r => r.CreateAsync(toDo2));
            _toDoRepositoryMock.Verify(r => r.CreateAsync(toDo3));
        }

        [Fact]
        public async Task DeleteListChildrenAsync_WhenToDosIsNull_DontCallCreateMethod()
        {
            var toDoList = new ToDoList { ToDos = null };
            await _toDoServiceImp.DeleteListChildrenAsync(toDoList);
            _toDoRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ToDo>()), Times.Never());
        }

        [Fact]
        public async Task DeleteListChildrenAsync_WhenToDosIsNotNull_CallMethodsBasedOnItems()
        {
            var toDo4 = new ToDo();
            var toDo3 = new ToDo() { Id = Guid.NewGuid() };
            var toDo2 = new ToDo() { Id = Guid.NewGuid() };
            var toDo1 = new ToDo() { Id = Guid.NewGuid() };
            var toDoList = new ToDoList { ToDos = new() { toDo1, toDo2 } };

            _toDoRepositoryMock.Setup(r => r.GetByIdAsync(toDo1.Id))
                .ReturnsAsync(toDo1);

            _toDoRepositoryMock.Setup(r => r.GetByIdAsync(toDo2.Id))
                .ReturnsAsync(toDo2);

            _toDoRepositoryMock.Setup(r => r.GetByParentIdAsync(toDo1.Id))
                .ReturnsAsync(new List<ToDo> { toDo3 });

            _toDoRepositoryMock.Setup(r => r.GetByParentIdAsync(toDo3.Id))
                .ReturnsAsync(new List<ToDo> { toDo4 });

            await _toDoServiceImp.DeleteListChildrenAsync(toDoList);
            _toDoRepositoryMock.Verify(r => r.MarkAsDeleted(toDo1));
            _toDoRepositoryMock.Verify(r => r.MarkAsDeleted(toDo2));
            _toDoRepositoryMock.Verify(r => r.MarkAsDeleted(toDo3));
            _toDoRepositoryMock.Verify(r => r.MarkAsDeleted(toDo4));
            _toDoRepositoryMock.Verify(r => r.SaveChangesAsync());
        }

        [Fact]
        public async Task PopulateToDosChildrenAsync_WhenCalled_PopulateChildren()
        {
            var listChildOfChild = new List<ToDo>();
            var listChild = new List<ToDo> { new() { Id = Guid.NewGuid() } };
            var list = new List<ToDo> { new() { Id = Guid.NewGuid() }, new() { Id = Guid.NewGuid() } };
            var lists = new ToDoList() { ToDos = list };

            _toDoRepositoryMock.Setup(r => r.GetByParentIdAsync(list[0].Id))
             .ReturnsAsync(listChild);

            _toDoRepositoryMock.Setup(r => r.GetByParentIdAsync(listChild[0].Id))
             .ReturnsAsync(listChildOfChild);

            var result = await _toDoServiceImp.PopulateToDosChildrenAsync(lists);

            Assert.Equal(listChild, result.ToDos?.First().Children);
            Assert.Equal(listChildOfChild, result.ToDos?.First().Children?.First().Children);
        }

        [Fact]
        public async Task UpdateListToDosAsync_WhenToDosIsNull_DontCallCreateOrUpdateMethod()
        {
            var toDoList = new UpdateToDoListDto { ToDos = null };
            await _toDoServiceImp.UpdateListToDosAsync(toDoList);
            _toDoRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<ToDo>()), Times.Never());
            _toDoRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ToDo>()), Times.Never());
        }

        [Fact]
        public async Task UpdateListToDosAsync_WhenAllTodosAreNew_CallSaveMethodsBasedOnItems()
        {
            var childOfChildToDo = new ToDo();
            var childToDo = new ToDo { Children = new() { childOfChildToDo } };
            var rootToDo = new ToDo { Children = new() { childToDo } };
            var siblingToDo = new ToDo();
            var list = new List<ToDo> { rootToDo, siblingToDo };
            var lists = new UpdateToDoListDto { ToDos = list };

            await _toDoServiceImp.UpdateListToDosAsync(lists);

            _toDoRepositoryMock.Verify(r => r.CreateAsync(rootToDo));
            _toDoRepositoryMock.Verify(r => r.CreateAsync(siblingToDo));
            _toDoRepositoryMock.Verify(r => r.CreateAsync(childToDo));
            _toDoRepositoryMock.Verify(r => r.CreateAsync(childOfChildToDo));
        }

        [Fact]
        public async Task UpdateListToDosAsync_WhenSomeTodosAreNewButNotOther_CallSaveMethodsBasedOnItems()
        {
            var childOfChildToDo = new ToDo();
            var childToDo = new ToDo { Id = Guid.NewGuid(), Name = "abc", IsDone = true, Index = 1, Children = new() { childOfChildToDo } };
            var childToDoInDb = new ToDo();
            var rootToDo = new ToDo { Id = Guid.NewGuid(), Name="abc", IsDone = true, Index = 1, Children = new() { childToDo } };
            var rootToDoInDb = new ToDo();
            var siblingToDo = new ToDo();
            var list = new List<ToDo> { rootToDo, siblingToDo };
            var lists = new UpdateToDoListDto { ToDos = list };

            _toDoRepositoryMock.Setup(r => r.GetByIdAsync(rootToDo.Id)).ReturnsAsync(rootToDoInDb);
            _toDoRepositoryMock.Setup(r => r.GetByIdAsync(childToDo.Id)).ReturnsAsync(childToDoInDb);

            await _toDoServiceImp.UpdateListToDosAsync(lists);

            _toDoRepositoryMock.Verify(r => r.UpdateAsync(It.Is<ToDo>(t => ToDosAreEquivalent(t, rootToDo))));
            _toDoRepositoryMock.Verify(r => r.CreateAsync(siblingToDo));
            _toDoRepositoryMock.Verify(r => r.UpdateAsync(It.Is<ToDo>(t => ToDosAreEquivalent(t, childToDo))));
            _toDoRepositoryMock.Verify(r => r.CreateAsync(childOfChildToDo));
        }

        private bool ToDosAreEquivalent(ToDo toDo1, ToDo toDo2)
        {
            return 
                toDo1.Name == toDo2.Name 
                && toDo1.IsDone == toDo2.IsDone 
                && toDo1.Index == toDo2.Index;
        }

        [Fact]
        public async Task DeleteAsync_WhenToDoDoesNotExist_DoNotCallDeleteMethods()
        {
            await _toDoServiceImp.DeleteAsync(Guid.NewGuid());
            _toDoRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ToDo>()), Times.Never());
        }

        [Fact]
        public async Task DeleteAsync_WhenToDoExists_CallMethodsBasedOnItems()
        {
            var childOfChildToDo = new ToDo();
            var childToDo = new ToDo { Id = Guid.NewGuid() };
            var toDo = new ToDo { Id = Guid.NewGuid() };
            _toDoRepositoryMock.Setup(r => r.GetByIdAsync(toDo.Id))                
                .ReturnsAsync(toDo);

            _toDoRepositoryMock.Setup(r => r.GetByParentIdAsync(toDo.Id))
               .ReturnsAsync(new List<ToDo> { childToDo }); 
            
            _toDoRepositoryMock.Setup(r => r.GetByParentIdAsync(childToDo.Id))
               .ReturnsAsync(new List<ToDo> { childOfChildToDo });

            await _toDoServiceImp.DeleteAsync(toDo.Id);

            _toDoRepositoryMock.Verify(r => r.DeleteAsync(toDo));
            _toDoRepositoryMock.Verify(r => r.MarkAsDeleted(childToDo));
            _toDoRepositoryMock.Verify(r => r.MarkAsDeleted(childOfChildToDo));
        }
    }
}
