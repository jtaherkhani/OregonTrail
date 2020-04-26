using OregonTrail.Data.Context;
using OregonTrail.Models.Shared;
using OregonTrail.UI.Server.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;

namespace OregonTrail.UI.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFileStorageService, AzureStorageService>();
            services.AddDbContext<OregonTrailDBContext>();
            
            // Expand the database to include security
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; // todo: Change this back before production
            })
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<OregonTrailDBContext>();

            services.AddIdentityServer()
                 .AddApiAuthorization<IdentityUser, OregonTrailDBContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            // creating a simple server options extension to configure our Jwt to require HTTPS (more will need to come later when we publish to azure.
            services.Configure<JwtBearerOptions>(IdentityServerJwtConstants.IdentityServerJwtBearerScheme, options => 
            {
                options.RequireHttpsMetadata = true;
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            // Create and bind options to configuration that will be used to seed data
            AdminOptions options = new AdminOptions();
            Configuration.GetSection("Admin").Bind(options);
            OregonTrailDBInitializer.SeedIdentity(roleManager, userManager, options);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages(); // razor pages for authentication are mapped to the server
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
