using Cycliko.IdentityServer.Options;
using Serilog;
using static System.Net.WebRequestMethods;

namespace Cycliko.IdentityServer
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            // uncomment if you want to add a UI
            builder.Services.AddRazorPages();


            // SecretsOptions & WebAppOptions ->  in appsettings.json;
            var identityOptions = builder.Configuration.GetSection(nameof(SecretsOptions));
            var apiSecret = identityOptions[nameof(SecretsOptions.SecretTokenApi)];
            var internSecret = identityOptions[nameof(SecretsOptions.SecretTokenIntern)];

            var webAppOptions = builder.Configuration.GetSection(nameof(WebAppOptions));
            var redirectUris = webAppOptions[nameof(WebAppOptions.RedirectUris)];
            var postLogoutUris = webAppOptions[nameof(WebAppOptions.PostLogoutRedirectUris)];

            var clientsService = new ClientsService(apiSecret, internSecret, redirectUris, postLogoutUris);

            

            builder.Services.AddIdentityServer(options =>
                {
                    // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                    options.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(clientsService.Clients)
                .AddTestUsers(TestUsers.Users);

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add a UI
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            // uncomment if you want to add a UI
            app.UseAuthorization();
            app.MapRazorPages().RequireAuthorization();

            return app;
        }
    }
}
