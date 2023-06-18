using Microsoft.EntityFrameworkCore;

namespace lab7.Entities
{
    public class DBContext : DbContext
    {

        public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
        public DBContext(DbContextOptions<DBContext> options)
           : base(options)
        {
        }
    }
}
