using Microsoft.EntityFrameworkCore;
using MvcTutorial.Models;

namespace MvcTutorial.Data
{
    public class ApplicationDbContext : DbContext //download DbContext from package Manager : Microsoft.EntityFrameworkCore
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
    }
}
