using CCSMDataManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace CCSMDataManager.Data
{
    public class CCMSAPPDbContext(DbContextOptions<CCMSAPPDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
