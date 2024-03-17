using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycliko.EnergyQuote.Api.Contracts.DTO
{
    public class EnergyQuoteResponseDTO
    {
        public double EnergyKiloJoules { get; set; }
        public double EnergyKcal { get => Math.Round( EnergyKiloJoules / 4.184, 2); }
    }
}
