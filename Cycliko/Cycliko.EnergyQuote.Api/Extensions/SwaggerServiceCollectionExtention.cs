using NSwag;

namespace Cycliko.EnergyQuote.Api.Extensions
{
    public static class SwaggerServiceCollectionExtention
    {
        public static IServiceCollection AddCyclikoOpenApiDoc(this IServiceCollection services)
        {
            services.AddOpenApiDocument(options => {
                options.PostProcess = document =>
                {
                    document.Info = new OpenApiInfo
                    {
                        Title = "Cycliko Energy Api",
                        Description = "Public Cycliko Api for race energy quotes.",
                        Contact = new OpenApiContact
                        {
                            Name = "Rafal Kowalski",
                            Email = "rafal.kowalski@student.hogent.be"
                        }
                    };
                };
            });

            return services;
        }
    }
}
