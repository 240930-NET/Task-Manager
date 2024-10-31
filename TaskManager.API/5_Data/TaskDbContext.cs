using Microsoft.EntityFrameworkCore;
using TaskManager.API.Model;

namespace TaskManager.API.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TaskModel> Tasks { get; set; }  // Maps TaskModel to the database
    }
}
