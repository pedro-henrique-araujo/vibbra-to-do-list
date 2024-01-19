namespace VibbraToDoList.Data.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string UserName { get; set; }

        public User()
        {
        }

        public User(string userName)
        {
            Id = Guid.NewGuid();
            UserName = userName;
        }
    }
}
