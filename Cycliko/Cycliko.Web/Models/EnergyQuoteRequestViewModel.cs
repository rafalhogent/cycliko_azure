using Cycliko.EnergyQuote.Api.Contracts.enums;

namespace Cycliko.Web.Models
{
    public class EnergyQuoteRequestViewModel
    {
        public double SpeedKph { get; set; }
        public double RiderHeightCm { get; set; }
        public double RiderWeightKg { get; set; }
        public double RaceDistanceKm { get; set; }
        public RaceTypeEnum RaceType { get; set; }
    }
}
