using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.Extensions.Options;

namespace Cycliko.IdentityServer
{
    public class ClientsService
    {

        private readonly string _apiSecret;
        private readonly string _internSecret;
        private readonly string _webAppRedirectUri;
        private readonly string _postLogoutRedirectUri;

        public ClientsService(string apiSecret, string internSecret, string webAppRedirectUri, string postLogoutRedirectUri)
        {
            _apiSecret = apiSecret;
            _internSecret = internSecret;
            _webAppRedirectUri = webAppRedirectUri;
            _postLogoutRedirectUri = postLogoutRedirectUri;
        }


        public IEnumerable<Client> Clients =>
            new Client[] {
                new Client {
                    ClientId = "cycliko-api-client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(_apiSecret.Sha256())},
                    AllowedScopes = { "cycliko.energyquote.api.WRITE", "cycliko.energyquote.api.READ" }
                },
                new Client
                {
                    ClientId = "internal-cycliko-invitations",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(_internSecret.Sha256())},
                    AllowedScopes = { "cycliko.energyquote.api.READ" }
                },
                new Client
                {
                    ClientId = "cycliko-web-client",
                    ClientSecrets = {new Secret(_internSecret.Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "cycliko.energyquote.api.READ"
                    },
                    RedirectUris = { _webAppRedirectUri },
                    PostLogoutRedirectUris = { _postLogoutRedirectUri },                    

                }
    };
    }
}
