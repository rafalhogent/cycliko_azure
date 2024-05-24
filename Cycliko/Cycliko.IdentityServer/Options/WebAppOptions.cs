namespace Cycliko.IdentityServer.Options
{
    public class WebAppOptions
    {
        public required string PostLogoutRedirectUris { get; set; }
        public required string RedirectUris { get; set; }
    }
}
