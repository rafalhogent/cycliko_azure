using Cycliko.EnergyQuote.Api.Contracts.DTO;
using Cycliko.EnergyQuote.Api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Cycliko.EnergyQuote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("cyclikoFixed")]
    public class EnergyQuoteController
    {
        private readonly IEnergyQuoteService _energyService;

        public EnergyQuoteController(IEnergyQuoteService energyService)
        {
            _energyService = energyService;
        }


        [HttpPost]
        public ActionResult<EnergyQuoteResponseDTO> CalculateEnergy([FromBody] EnergyQuoteRequestDTO request)
        {
            var energy = _energyService.Calculate(request);
            return energy;
        }
    }
}