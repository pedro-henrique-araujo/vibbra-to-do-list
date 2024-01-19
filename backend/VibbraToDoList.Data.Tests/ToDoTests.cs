using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Data.Tests
{
    public class ToDoTests
    {
        [Fact]
        public void Initialize_WhenIdIsNotEmpty_DoNotChangeId()
        {
            var toDoId = Guid.NewGuid();
            var toDo = new ToDo { Id = toDoId };
            toDo.Initialize();
            Assert.Equal(toDoId, toDo.Id);

        }

        [Fact]
        public void Initialize_WhenIdIsEmpty_GenerateId() 
        {
            var toDo = new ToDo();
            toDo.Initialize();
            Assert.NotEqual(Guid.Empty, toDo.Id);
        }
    }
}
