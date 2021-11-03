using Microsoft.EntityFrameworkCore;
using TreeTasksStep.Domain;

namespace TreeTasksStep.Persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Step> Steps { get; set; }
    }
}
