using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cycliko.EnergyQuote.Storage
{
    public class EnergyQuoteRepoOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ContainerId { get; set; }
    }
}
