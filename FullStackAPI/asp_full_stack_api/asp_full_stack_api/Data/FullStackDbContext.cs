using asp_full_stack_api.Models;
using Microsoft.EntityFrameworkCore;
namespace asp_full_stack_api.Data
{
    public class FullStackDbContext : DbContext
    {
        
        public FullStackDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
