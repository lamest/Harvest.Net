using System.Threading.Tasks;
using Xunit;

namespace Harvest.Net.Tests.ResourceFacts
{
    public class ClientFacts : FactBase
    {
        [Fact]
        public async Task GetClients_ReturnsANewToken()
        {
            var clientsContainer = await Api.Clients.ListAllAsync();
            Assert.NotNull(clientsContainer);
            var t = clientsContainer.Clients;
        }
    }
}
