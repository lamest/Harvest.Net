using System.Threading.Tasks;
using Harvest.Net.Containers;
using Harvest.Net.Models;
using Refit;

namespace Harvest.Net.Resources
{
    public interface IToken
    {
        [Post("/api/v2/oauth2/token")]
        Task<Token> Refresh(
            [AliasAs("refresh_token")] string refreshToken,
            [AliasAs("client_id")] string clientId,
            [AliasAs("client_secret")] string clientSecret,
            [AliasAs("grant_type")] string grantType = "refresh_token"
        );
    }
}