using Microsoft.EntityFrameworkCore;

namespace Lab10
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
