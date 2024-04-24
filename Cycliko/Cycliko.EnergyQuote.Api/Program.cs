using Cycliko.EnergyQuote.Api.Service;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using System.Text.Json.Serialization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cycliko.EnergyQuote.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter());
            });
            ;

            builder.Services.AddRateLimiter(rl =>
            {
                rl.AddFixedWindowLimiter(policyName: "cyclikoFixed", options =>
                {
                    options.PermitLimit = 3;
                    options.Window = TimeSpan.FromSeconds(10);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 0;

                });
            });

            var quoteService = new EnergyQuoteService(9.81, 1, 0.3, 0.0032, 10);

            builder.Services.AddSingleton<IEnergyQuoteService>(quoteService);

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
