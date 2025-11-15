using GameFun.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GameFun.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevice>()
                .HasKey(gd => new { gd.GameId, gd.DeviceId });


            // Seeding  Category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action" },
                new Category { Id = 2, Name = "Adventure" },
                new Category { Id = 3, Name = "RPG" },
                new Category { Id = 4, Name = "Strategy" },
                new Category { Id = 5, Name = "Simulation" }
            );

            // Seeding Device
            modelBuilder.Entity<Device>().HasData(
                new Device { Id = 1, Name = "PC" },
                new Device { Id = 2, Name = "PlayStation" },
                new Device { Id = 3, Name = "Xbox" },
                new Device { Id = 4, Name = "Nintendo Switch" },
                new Device { Id = 5, Name = "Mobile" }
            );
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }


    }
}
