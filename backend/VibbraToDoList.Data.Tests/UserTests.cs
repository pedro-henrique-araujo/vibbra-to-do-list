using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Data.Tests
{
    public class UserTests
    {
        [Fact]
        public void Constuctor_UserNameParameter_GenerateId()
        {
            var user = new User("a");
            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.Equal("a", user.UserName);
        }

        [Fact]
        public void Constuctor_NoPararmeters_GenerateEmpty()
        {
            var user = new User();
            Assert.Equal(Guid.Empty, user.Id);
            Assert.Null(user.UserName);
        }
    }
}