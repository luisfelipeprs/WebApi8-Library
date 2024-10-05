using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApi8_Library.Models;

namespace WebApi8_Library.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }
        public DbSet<AuthorModel> Author { get; set; }
        public DbSet<BookModel> Book { get; set; }
    }
}