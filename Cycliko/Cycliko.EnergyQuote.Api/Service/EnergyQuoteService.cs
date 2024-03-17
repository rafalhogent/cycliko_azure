using Cycliko.EnergyQuote.Api.Contracts.DTO;

namespace Cycliko.EnergyQuote.Api.Service
{
    public class EnergyQuoteService : IEnergyQuoteService
    {
        private readonly double _airDensity;
        private readonly double _cdAFactor;
        private readonly double _rollingResistance;
        private readonly double _bikeWeightKg;
        private readonly double _gravityAccelerationMs2;

        public EnergyQuoteService(double gravityAccelerationMs2, double airDensity, double cdAFactor, double rollingResistance, double bikeWeightKg)
        {
            _airDensity = airDensity;
            _rollingResistance = rollingResistance;
            _bikeWeightKg = bikeWeightKg;
            _cdAFactor = cdAFactor;
            _gravityAccelerationMs2 = gravityAccelerationMs2;

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

            var aeroDrag = 0.5 * _airDensity * Math.Pow(request.SpeedKph / 3.6, 2) * _cdAFactor * (request.RiderHeightCm / 175);

            var rollDrag = _rollingResistance * (request.RiderWeightKg + _bikeWeightKg);

            var totalEnergyKJ = (aeroDrag + rollDrag) * request.RaceDistanceKm * _gravityAccelerationMs2;

            return new EnergyQuoteResponseDTO { EnergyKiloJoules = Math.Round(totalEnergyKJ, 2) };
        }
    }
}
