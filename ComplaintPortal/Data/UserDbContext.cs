using ComplaintPortal.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ComplaintPortal.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
