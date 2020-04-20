using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OregonTrail.Models;
using System;

namespace OregonTrail.Data.Context
{
    public class OregonTrailDBContext : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile(@"C:\Users\jtaherkhani\Source2\repos\OregonTrail.Data\data_appsettings.json", // todo: Need to investigate how to ensure this finds correctly when published to the app service.
                    optional: true, reloadOnChange: true);

                var Configuration = builder.Build();
                string connstr = Configuration.GetConnectionString("AzureConnection");
                optionsBuilder.UseSqlServer(connstr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            SeedData(modelBuilder);

        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "dummy", Points = 10, Image = "DummyImage.jpg" });
        }
    }
}
