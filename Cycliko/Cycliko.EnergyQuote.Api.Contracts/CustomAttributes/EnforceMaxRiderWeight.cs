using Cycliko.EnergyQuote.Api.Contracts.DTO;
using Cycliko.EnergyQuote.Api.Contracts.enums;
using System.ComponentModel.DataAnnotations;


namespace Cycliko.EnergyQuote.Api.Contracts.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EnforceMaxRiderWeight : ValidationAttribute
    {
        public EnforceMaxRiderWeight(RaceTypeEnum raceType, double riderMaxWeightKg)
        {
            RiderMaxWeightKg = riderMaxWeightKg;
            RaceType = raceType;
        }

        public double RiderMaxWeightKg { get; set; }
        public RaceTypeEnum RaceType { get; set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var quoteRequest = (EnergyQuoteRequestDTO)validationContext.ObjectInstance;
            if(quoteRequest.RaceType == RaceType && quoteRequest.RiderWeightKg > RiderMaxWeightKg)
            {
                return new ValidationResult("The rider is too heavy, competitor too heavy, cannot participate for safety reasons");
            }
            return ValidationResult.Success;
        }
    }
}
