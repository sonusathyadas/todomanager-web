using Microsoft.EntityFrameworkCore;

namespace TodoManager.Models
{
    public class TodoDataContext:DbContext
    {
        public TodoDataContext(DbContextOptions<TodoDataContext> options)
        :base(options)
        {

        }

        public DbSet<Todo> Todos {get;set;}
    }
}