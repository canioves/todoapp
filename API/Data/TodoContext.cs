using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TodoContext : DbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>().Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }
}
