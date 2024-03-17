using Cycliko.EnergyQuote.Api.Contracts.DTO;

namespace Cycliko.EnergyQuote.Api.Service
{
    public interface IEnergyQuoteService
    {
        EnergyQuoteResponseDTO Calculate(EnergyQuoteRequestDTO request);
    }
}