using Cycliko.EnergyQuote.Api.Contracts.DTO;
using Cycliko.EnergyQuote.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Cycliko.EnergyQuote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyQuoteController
    {
        private readonly IEnergyQuoteService _energyService;

        public EnergyQuoteController(IEnergyQuoteService energyService)
        {
            _energyService = energyService;
        }

        public ActionResult<EnergyQuoteResponseDTO> CalculateEnergy(EnergyQuoteRequestDTO request)
        {
            var energy = _energyService.Calculate(request);
            return energy;
        }
    }
}