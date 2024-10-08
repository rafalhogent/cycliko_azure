﻿using Cycliko.EnergyQuote.Api.Contracts.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycliko.EnergyQuote.Api.Contracts.DTO
{
    public class EnergyQuoteResponseDTO
    {
        public string Id { get; set; }
        public double SpeedKph { get; set; }
        public double RiderHeightCm { get; set; }
        public double RiderWeightKg { get; set; }
        public double RaceDistanceKm { get; set; }
        public RaceTypeEnum RaceType { get; set; }

        public double EnergyKiloJoules { get; set; }
        public double EnergyKcal { get => Math.Round(EnergyKiloJoules / 4.184, 2); }
    }
}
