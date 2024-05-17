using NSwag;
namespace Cycliko.Invitation.API.Extensions
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
                        Title = "Cycliko Invitations Api",
                        Description = "Public Cycliko Api for race invitations.",
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
