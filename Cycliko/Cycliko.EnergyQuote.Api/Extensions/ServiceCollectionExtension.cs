using Cycliko.EnergyQuote.Api.Options;
using Cycliko.EnergyQuote.Api.Service;
using Cycliko.EnergyQuote.Storage;
using Microsoft.AspNetCore.RateLimiting;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

namespace Cycliko.EnergyQuote.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCyclikoOptions(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<EnergyQuoteServiceOptions>(
                configuration.GetSection(key: nameof(EnergyQuoteServiceOptions)));
            services.Configure<EnergyQuoteRepoOptions>(
                configuration.GetSection(key: nameof(EnergyQuoteRepoOptions)));
            services.Configure<IdentityOptions>(configuration.GetSection(key: nameof(IdentityOptions)));

            return services;
        }

        public static IServiceCollection AddCyclikoEnergyServices(this IServiceCollection services)
        {
            services.AddScoped<IEnergyQuoteRepo, EnergyQuoteRepo>();
            services.AddScoped<IEnergyQuoteService, EnergyQuoteService>();

            return services;
        }

        public static IServiceCollection AddCyclikoRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(rl =>
            {
                rl.AddFixedWindowLimiter(policyName: "cyclikoFixed", options =>
                {
                    options.PermitLimit = 3;
                    options.Window = TimeSpan.FromSeconds(10);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 0;

                });
            });

            return services;
        }

        public static IServiceCollection AddCyclikoControllers(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter());
            }).AddMvcOptions(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            return services;
        }

    }


}
