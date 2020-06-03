using Microsoft.EntityFrameworkCore;

namespace Conference.Logging.Data
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options)
            : base(options)
        {
           // this.Database.EnsureCreated();

        }
        public virtual DbSet<Log> Logs { get; set; }
    }
}
