using MariamPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace MariamPortfolio.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Blogpost> Blogposts { get; set; }
    }
}