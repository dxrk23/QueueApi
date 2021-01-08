using Microsoft.EntityFrameworkCore;
using QueueApi.Models;

namespace QueueApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
