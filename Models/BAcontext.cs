using Microsoft.EntityFrameworkCore;
 
namespace bank_accounts.Models
{
    public class BAContext : DbContext
    {
        public BAContext(DbContextOptions<BAContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Record> records { get; set; }
        
        
    }
}
