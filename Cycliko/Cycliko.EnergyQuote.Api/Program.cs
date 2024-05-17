using Cycliko.EnergyQuote.Api.Service;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using System.Text.Json.Serialization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cycliko.EnergyQuote.Api.Options;
using Cycliko.EnergyQuote.Storage;
using Cycliko.EnergyQuote.Api.Extensions;

namespace Cycliko.EnergyQuote.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCyclikoControllers();
            builder.Services.AddCyclikoOptions(builder.Configuration);
            builder.Services.AddCyclikoRateLimiter();
            builder.Services.AddCyclikoEnergyServices();

            builder.Services.AddOpenApiDocument();

            var app = builder.Build();
            app.MapControllers();
            app.UseRateLimiter();

            app.UseOpenApi();
            app.UseSwaggerUi();

            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/error-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.Run();
        }
    }
}
