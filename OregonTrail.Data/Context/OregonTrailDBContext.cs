using IdentityModel;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OregonTrail.Models.Shared;
using OregonTrail.Models.Shared.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OregonTrail.Data.Context
{
    public class OregonTrailDBContext : ApiAuthorizationDbContext<IdentityUser>
    {
        public OregonTrailDBContext(DbContextOptions<OregonTrailDBContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

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
    }
}
