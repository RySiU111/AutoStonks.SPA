using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using AutoStonks.SPA.Services;
using AutoStonks.SPA.Services.BrandService;
using AutoStonks.SPA.Services.AuthService;

namespace AutoStonks.SPA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
                .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
                .AddScoped<IAdvertService, AdvertService>()
                .AddScoped<IBrandService, BrandService>()
                .AddScoped<IAuthService, AuthService>()
                .AddBlazorise( options =>
                    {
                        options.ChangeTextOnKeyPress = true;
                    })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
