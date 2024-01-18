using Microsoft.EntityFrameworkCore;
using Mango.Services.ProductAPI.Models;
namespace Mango.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }
        public DbSet<Product> Products { get; set; }
    }
}
