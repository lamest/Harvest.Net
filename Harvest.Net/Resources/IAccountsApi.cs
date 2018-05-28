using System.Threading.Tasks;
using Harvest.Net.Containers;
using Refit;

namespace Harvest.Net.Resources
{
    [Headers("Authorization: Bearer")]
    public interface IAccountsApi
    {
        [Get("/api/v2/accounts")]
        Task<AccountsContainer> ListAllAsync();
    }
}