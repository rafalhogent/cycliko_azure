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
            builder.Services.AddCyclikoOpenApiDoc();

            // IdentityOptions -> AuthorityUrl in appsettings.json;
            var identityOptions = builder.Configuration.GetSection(nameof(IdentityOptions));
            var authorityUrl = identityOptions[nameof(IdentityOptions.AuthorityUrl)];

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = authorityUrl;
                    options.TokenValidationParameters.ValidateAudience = false;
                });
            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("EnergyQuoteReadPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "cycliko.energyquote.api.READ");
                })
                .AddPolicy("EnergyQuoteWritePolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "cycliko.energyquote.api.WRITE");
                });

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
