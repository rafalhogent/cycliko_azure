using Duende.IdentityServer.Models;
using Microsoft.Extensions.Options;

namespace Cycliko.IdentityServer
{
    public class ClientsService
    {

        private readonly string _apiSecret;
        private readonly string _internSecret;

        public ClientsService(string apiSecret, string internSecret)
        {
            _apiSecret = apiSecret;
            _internSecret = internSecret;
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
                }
    };
    }
}
