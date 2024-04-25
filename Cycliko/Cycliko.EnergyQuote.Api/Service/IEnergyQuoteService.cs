using Cycliko.EnergyQuote.Api.Contracts.DTO;

namespace Cycliko.EnergyQuote.Api.Service
{
    public interface IEnergyQuoteService
    {
        Task<EnergyQuoteResponseDTO> CreateEnergyQuoteAsync(EnergyQuoteRequestDTO request);
        Task<EnergyQuoteResponseDTO?> GetEnergyQuoteAsync(string id);
    }
}