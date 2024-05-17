using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Cycliko.EnergyQuote.Api.Tests
{
    public class EnergyQuoteControllerTests: IClassFixture<WebApplicationFactory<Cycliko.EnergyQuote.Api.Program>>
    {
        private readonly WebApplicationFactory<Cycliko.EnergyQuote.Api.Program> _factory;

        public EnergyQuoteControllerTests(WebApplicationFactory<Cycliko.EnergyQuote.Api.Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void GetExistingQuoteShouldReturnOk()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/energyquote/a373caa4-b901-42a7-bbf5-2e580b132006");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}