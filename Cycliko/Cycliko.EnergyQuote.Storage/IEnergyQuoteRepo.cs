using Cycliko.EnergyQuote.Storage.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycliko.EnergyQuote.Storage
{
    public interface IEnergyQuoteRepo
    {
        Task<EnergyQuoteModel> CreateAsync(EnergyQuoteModel quote);
        Task<EnergyQuoteModel?> GetAsync(string id);
    }
}
