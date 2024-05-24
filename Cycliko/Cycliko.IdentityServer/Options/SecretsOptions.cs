namespace Cycliko.IdentityServer.Options
{
    public class SecretsOptions
    {
        public required string SecretTokenApi { get; set; }
        public required string SecretTokenIntern { get; set; }
    }
}
