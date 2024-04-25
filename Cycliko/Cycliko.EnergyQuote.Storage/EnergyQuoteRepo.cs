using Cycliko.EnergyQuote.Storage.Contracts;
using Microsoft.Azure.Cosmos;
using System.Data.Common;
using Microsoft.Extensions.Options;


namespace Cycliko.EnergyQuote.Storage
{
    public class EnergyQuoteRepo : IEnergyQuoteRepo
    {
        private readonly EnergyQuoteRepoOptions _options;
        public EnergyQuoteRepo(IOptions<EnergyQuoteRepoOptions> options)
        {
            _options = options.Value;
        }

        public async Task<EnergyQuoteModel> CreateAsync(EnergyQuoteModel quote)
        {

            CosmosClient client = new CosmosClient(_options.ConnectionString);
            Database database = client.GetDatabase(_options.DatabaseName);

            ContainerProperties containerProperties = new ContainerProperties()
            {
                Id = _options.ContainerId,
                PartitionKeyPath = "/id"
            };

            Container container = await database.CreateContainerIfNotExistsAsync(containerProperties);

            return await container.CreateItemAsync(item: quote, partitionKey: new PartitionKey(quote.id));
        }

        public async Task<EnergyQuoteModel?> GetAsync(string id)
        {
            CosmosClient client = new CosmosClient(_options.ConnectionString);

            Database database = client.GetDatabase(_options.DatabaseName);

            ContainerProperties containerProperties = new ContainerProperties()
            {
                Id = _options.ContainerId,
                PartitionKeyPath = "/id"
            };

            Container container = await database.CreateContainerIfNotExistsAsync(containerProperties);

            try
            {
                return await container.ReadItemAsync<EnergyQuoteModel>(
                    id: id,
                    partitionKey: new PartitionKey(id)
                );

            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
