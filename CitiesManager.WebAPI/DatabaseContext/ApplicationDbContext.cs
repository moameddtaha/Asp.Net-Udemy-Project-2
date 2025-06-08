using CitiesManager.WebAPI.Modles;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.WebAPI.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>()
                .HasData(
                    new City { CityId = Guid.Parse("55416906-1268-40EC-A210-3B74421945D7"), CityName = "New York" }
                );

            modelBuilder.Entity<City>()
                .HasData(
                    new City { CityId = Guid.Parse("48203632-DB4F-4275-B9CD-1994AF76286E"), CityName = "Los Angeles" }
                );
        }
    }
}
