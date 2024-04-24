﻿using Cycliko.EnergyQuote.Api.Contracts.DTO;
using Microsoft.Extensions.Options;
using Cycliko.EnergyQuote.Api.Options;

namespace Cycliko.EnergyQuote.Api.Service
{
    public class EnergyQuoteService : IEnergyQuoteService
    {
      
        private readonly EnergyQuoteServiceOptions _options;
        public EnergyQuoteService(IOptions<EnergyQuoteServiceOptions> options)
        {
            _options = options.Value;
        }

        public EnergyQuoteResponseDTO Calculate(EnergyQuoteRequestDTO request)
        {
            /* 
                Aerodynamic Drag:
                    Fd = 0.5 * CdA * ro * (v)²
                        CdA = 0.3 * (RiderHeight / 1.75)  -  Aerodynamic factor and frontal area
                        ro = 1kg/m3  air density
                        v - rider speed (expected or effective)     

                Rolling resistance
	                Fr = g * (M + m) * Crr
                        g = gravitational acceleration (standard value of 9.81 m/s2)
                        M = weight of the rider (kg)
                        m = weight the bike (kg) = 7kg
                        Crr = coefficient of rolling resitance = 0.0032
             */


            var aeroDrag = 0.5 * _options.AirDensity * Math.Pow(request.SpeedKph / 3.6, 2) * _options.CdAFactor * (request.RiderHeightCm / _options.AvgRiderHeightCm);

            var rollDrag = _options.RollingResistance * (request.RiderWeightKg + _options.BikeWeightKg);

            var totalEnergyKJ = (aeroDrag + rollDrag) * request.RaceDistanceKm * _options.GravityAccMs2;

            if (totalEnergyKJ < 0 )
            {
                throw new Exception("Energy calculation result out of acceptable range.");
            }
            return new EnergyQuoteResponseDTO { EnergyKiloJoules = Math.Round(totalEnergyKJ, 2) };
        }
    }
}
