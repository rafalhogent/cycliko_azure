using Cycliko.Web.Options;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Cycliko.Web.Pages
{
    public class GetEnergyQuoteModel : PageModel
    {
        public string Json { get; set; } = string.Empty;
        public string? qID { get; set; } = string.Empty;

        public async Task OnGet(string? id)
        {

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var response = await client.GetAsync($"https://localhost:7246/api/EnergyQuote/{id}");
            var stringContent = await response.Content.ReadAsStringAsync();

            qID = id;
            Json = stringContent;

        }

    }
}
