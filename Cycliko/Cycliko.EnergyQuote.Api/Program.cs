using Cycliko.EnergyQuote.Api.Service;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using System.Text.Json.Serialization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cycliko.EnergyQuote.Api.Options;
using Cycliko.EnergyQuote.Storage;

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
            }).AddMvcOptions(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
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

            builder.Services.Configure<EnergyQuoteServiceOptions>(
                builder.Configuration.GetSection(key: nameof(EnergyQuoteServiceOptions)));

            builder.Services.Configure<EnergyQuoteRepoOptions>(
                builder.Configuration.GetSection(key: nameof(EnergyQuoteRepoOptions)));

            builder.Services.AddScoped<IEnergyQuoteRepo, EnergyQuoteRepo>();
            builder.Services.AddScoped<IEnergyQuoteService, EnergyQuoteService>();

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
