using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Data.Tests
{
    public class ToDoListTests
    {
        [Fact]
        public void Initialize_WhenIdIsNotEmpty_DoNotChangeId()
        {
            var listId = Guid.NewGuid();
            var list = new ToDoList { Id = listId };
            list.Initialize();
            Assert.Equal(listId, list.Id);

        }

        [Fact]
        public void Initialize_WhenIdIsEmpty_GenerateId()
        {
            var list = new ToDoList();
            list.Initialize();
            Assert.NotEqual(Guid.Empty, list.Id);
        }
    }
}
