
using DomainModelsLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Repositories.ConfigurationEntities;

namespace Repositories.APPDPCONTEXT
{
    public class AppDbContext :DbContext
    {
        public DbSet<Book> books { get; set; }
        public DbSet<Author> authors { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() { }


       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfiguration).Assembly);
        }





    }
}
