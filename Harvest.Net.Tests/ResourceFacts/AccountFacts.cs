using System.Threading.Tasks;
using Xunit;

namespace Harvest.Net.Tests.ResourceFacts
{
    public class AccountFacts : FactBase
    {
        [Fact]
        public async Task GetAccounts_Returns()
        {
            var accountsContainer = await Api.Account.ListAllAsync();
            Assert.NotNull(accountsContainer);
            var t = accountsContainer.Accounts;
            var d = accountsContainer.User;
        }
    }
}