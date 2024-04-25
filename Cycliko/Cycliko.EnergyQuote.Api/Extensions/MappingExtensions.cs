using Azure.Core;
using Cycliko.EnergyQuote.Api.Contracts.DTO;
using Cycliko.EnergyQuote.Storage.Contracts;
using System.Runtime.CompilerServices;

namespace Cycliko.EnergyQuote.Api.Extensions
{
    public static class MappingExtensions
    {
        public static EnergyQuoteModel MapToModel(this EnergyQuoteRequestDTO request)
        {
            return new EnergyQuoteModel
            {
                RaceType = Enum.Parse<Storage.Contracts.enums.RaceTypeEnum>(request.RaceType.ToString()),
                SpeedKph = request.SpeedKph,
                RiderHeightCm = request.RiderHeightCm,
                RiderWeightKg = request.RiderWeightKg,
                RaceDistanceKm = request.RaceDistanceKm,
            };
        }

        public static EnergyQuoteResponseDTO MapToResponse(this EnergyQuoteModel model)
        {
            return new EnergyQuoteResponseDTO
            {
                Id = model.id,
                RaceType = Enum.Parse<Api.Contracts.enums.RaceTypeEnum>(model.RaceType.ToString()),
                SpeedKph = model.SpeedKph,
                RiderHeightCm = model.RiderHeightCm,
                RiderWeightKg = model.RiderWeightKg,
                RaceDistanceKm = model.RaceDistanceKm,

                EnergyKiloJoules = model.EnergyKiloJoules
            };
        }
    }
}
