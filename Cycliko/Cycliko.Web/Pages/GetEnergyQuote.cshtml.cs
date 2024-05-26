using Cycliko.EnergyQuote.Api.Contracts.DTO;
using Cycliko.Web.Options;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Cycliko.Web.Pages
{
    public class GetEnergyQuoteModel : PageModel
    {
        public string Json { get; set; } = string.Empty;
        public string? qID { get; set; } = string.Empty;
        public string? EnergyResp { get; set; } = string.Empty;

        private readonly IOptions<WebAppOptions> _options;

        public GetEnergyQuoteModel(IOptions<WebAppOptions> options)
        {
            _options = options;
        }

        public async Task OnGet(string? id)
        {

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var response = await client.GetAsync($"{_options.Value.EnergyQuoteUri}/api/EnergyQuote/{id}");
            var stringContent = await response.Content.ReadAsStringAsync();

            var jsonoptions = new JsonSerializerOptions();
            jsonoptions.PropertyNameCaseInsensitive = true;
            jsonoptions.Converters.Add(new JsonStringEnumConverter());
            var inhoud = await response.Content.ReadFromJsonAsync<EnergyQuoteResponseDTO>(jsonoptions);

            qID = id;
            Json = stringContent;
            EnergyResp = inhoud?.EnergyKiloJoules.ToString();


        }

    }
}
