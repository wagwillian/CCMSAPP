using CCSMDataManager.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CCSMDataManager.Data
{
    public class CCMSAPPDbContext(DbContextOptions<CCMSAPPDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RelatorioSala> RelatorioSalas { get; set; }
    }
}
