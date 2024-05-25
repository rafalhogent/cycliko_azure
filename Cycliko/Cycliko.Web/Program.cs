using Cycliko.Web.Options;

namespace Cycliko.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // SecretsOptions ->  in appsettings.json;
            var appOptions = builder.Configuration.GetSection(nameof(WebAppOptions));
            var clientSecret = appOptions[nameof(WebAppOptions.ClientSecretToken)];
            var authUri = appOptions[nameof(WebAppOptions.AuthorityUri)];

            builder.Services.Configure<WebAppOptions>(builder.Configuration.GetSection(key: nameof(WebAppOptions)));

            // Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = authUri;

                options.ClientId = "cycliko-web-client";
                options.ClientSecret = clientSecret;
                options.ResponseType = "code";

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("cycliko.energyquote.api.READ");

                options.GetClaimsFromUserInfoEndpoint = true;
                options.MapInboundClaims = false;

                options.SaveTokens = true;
            });





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages().RequireAuthorization();

            app.Run();
        }
    }
}
