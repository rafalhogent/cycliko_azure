using Cycliko.EnergyQuote.Storage.Contracts.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycliko.EnergyQuote.Storage.Contracts
{
    public class EnergyQuoteModel
    {
        public string id { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        public double SpeedKph { get; set; }
        public double RiderHeightCm { get; set; }
        public double RiderWeightKg { get; set; }
        public double RaceDistanceKm { get; set; }
        public RaceTypeEnum RaceType { get; set; }

        public double EnergyKiloJoules { get; set; }
    }
}
