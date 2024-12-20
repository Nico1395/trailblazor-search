using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Trailblazor.Search.App.Persistence;
using Trailblazor.Search.App.Search;
using Trailblazor.Search.App.Search.Handlers;
using Trailblazor.Search.DependencyInjection;

namespace Trailblazor.Search.App;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ISystemLogRepository, SystemLogRepository>();

        builder.Services.AddTrailblazorSearch(options =>
        {
            options.AddOperation<UniversalSearchRequest>("universal-search", p =>
            {
                p.WithThread("th-a", t =>
                {
                    t.WithHandler<UserSearchRequestHandler>();
                    t.WithHandler<ProductSearchRequestHandler>(1);
                });
                p.WithThread("th-b", t =>
                {
                    t.WithHandler<SystemLogSearchRequestHandler>();
                });
            });

            options.AddOperation<ProductSearchRequest>("products-search", p =>
            {
                p.WithThread("th-a", t => t.WithHandler<ProductSearchRequestHandler>());
            });
        });

        await builder.Build().RunAsync();
    }
}
