using Cycliko.EnergyQuote.Api.Contracts.DTO;
using Cycliko.Web.Options;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using Cycliko.Web.Models;

namespace Cycliko.Web.Pages
{
    public class SendEnergyQuoteModel : PageModel
    {
        private readonly IOptions<WebAppOptions> _options;

        [BindProperty]
        public EnergyQuoteRequestViewModel RequestViewModel { get; set; }
        public string ErrorMsg { get; set; } = "";

        public SendEnergyQuoteModel(IOptions<WebAppOptions> options)
        {
            _options = options;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.PropertyNameCaseInsensitive = true;
            jsonOptions.Converters.Add(new JsonStringEnumConverter());

            try
            {
                var response = await client.PostAsJsonAsync($"{_options.Value.EnergyQuoteUri}/api/EnergyQuote/", RequestViewModel, jsonOptions);
                var stringContent = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;
                options.Converters.Add(new JsonStringEnumConverter());
                var inhoud = await response.Content.ReadFromJsonAsync<EnergyQuoteResponseDTO>(options);

                return RedirectToPage("./GetEnergyQuote/", new { id = inhoud.Id.ToString() });

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }


            return Page();
        }
    }
}
