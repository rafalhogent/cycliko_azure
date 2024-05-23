using Duende.IdentityServer.Models;

namespace Cycliko.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope(name: "cycliko.energyquote.api.READ", displayName: "Cycliko Energy Api READ"),
                new ApiScope(name: "cycliko.energyquote.api.WRITE", displayName: "Cycliko Energy Api WRITE")
            };


       // (Clients moved to apart service)
    }
}