using Blazor.FileReader;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OregonTrail.UI.Client.Services;
using Radzen;
using System.Threading.Tasks;

namespace OregonTrail.UI.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        /// <summary>
        /// Add services for dependency injection.
        /// </summary>
        /// <param name="services">The services being built.</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddBaseAddressHttpClient();
            services.AddFileReaderService();
            services.AddSweetAlert2(options => {
                options.Theme = SweetAlertTheme.Bootstrap4; // map the sweet alert theme to bootstrap
            });

            services.AddScoped<ItemService>();
            services.AddScoped<DialogService>();
        }
    }
}
