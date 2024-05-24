using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;

namespace Cycliko.Web.Pages
{
    public class RequestEnergyQuoteModel : PageModel
    {
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var quoteid = Request.Form["quoteid"];
            return RedirectToPage("./GetEnergyQuote/", new { id = quoteid.ToString() });
           
        }
    }
}
