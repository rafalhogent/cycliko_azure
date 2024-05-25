namespace Cycliko.Web.Options
{
    public class WebAppOptions
    {
        public required string ClientSecretToken { get; set; }
        public required string AuthorityUri { get; set; }
        public required string EnergyQuoteUri { get; set; }
    }
}
