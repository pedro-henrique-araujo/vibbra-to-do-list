using Microsoft.EntityFrameworkCore;
using VibbraToDoList.Data.Models;

namespace VibbraToDoList.Data
{
    public class VibbraToDoListDbContext : DbContext
    {
        public VibbraToDoListDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoListUser> ToDoListsUsers { get; set; }
        public DbSet<ToDo> ToDos {  get; set; }
    }
}