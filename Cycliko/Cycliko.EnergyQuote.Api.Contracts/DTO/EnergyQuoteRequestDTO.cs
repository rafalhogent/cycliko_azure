﻿using Cycliko.EnergyQuote.Api.Contracts.CustomAttributes;
using Cycliko.EnergyQuote.Api.Contracts.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycliko.EnergyQuote.Api.Contracts.DTO
{
    [EnforceMaxRiderWeight(RaceTypeEnum.Mountainous, 100)]
    [EnforceMaxRiderWeight(RaceTypeEnum.Hilly, 110)]
    [EnforceMaxRiderWeight(RaceTypeEnum.Flat, 120)]
    public class EnergyQuoteRequestDTO
    {
        [Required]
        [Range(0.1d, 100d)]
        public double SpeedKph { get; set; }

        [Required]
        [Range(50d, 280d)]
        public double RiderHeightCm { get; set; }

        [Required]
        [Range(10d, 250d)]
        public double RiderWeightKg { get; set; }

        [Required]
        [Range(0.1d, 1000d)]
        public double RaceDistanceKm { get; set; }

        public RaceTypeEnum RaceType { get; set; } = RaceTypeEnum.Flat;

    }
}
