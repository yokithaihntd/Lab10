using Microsoft.EntityFrameworkCore;

namespace Lab10
{
    public class DataContext : AppDbContext
    {
        public DbSet<Film> Films { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=lab10;Username=postgres;Password=1111;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
