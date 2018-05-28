using System;
using System.Threading.Tasks;
using Harvest.Net.Models;
using Xunit;

namespace Harvest.Net.Tests.ResourceFacts
{
    public class TokenFacts : FactBase
    {
        private readonly Token _token = new Token
        {
            AccessToken = "1553055.at.cgs5LoOodzMDyLPXfBYAjZ6goAsjwkequaVlDcpO3JYXeMq20ksfzIPER_xunctwP3CjbKnUmWMk_Aa0zxjuIw",
            ExpiresIn = 1209599,
            RefreshToken = "1553055.rt.FFotVlaicxQhZ_CRfaiyjC5Wlljq4e9IJonVI7hwSjq6lMeZXbhnU6jr9ZKuIsUYjfTahByNJ9BchA_9WD2uxQ",
            TokenType = "bearer"
        };

        private readonly string _clientId = "pvUgpvtUeSpJD84RB3597tm9";
        private readonly string _clientSecret = "XgRC52VdIZbBRMsNraDG0KJH8RT26zaiImLoBc8TRTBv6M5ae5I9G_hKQvY78rOzhrVVACQmtd_SEwZeDsIncw";

        [Fact]
        public async Task RefreshToken_ReturnsANewToken()
        {
            var token = await Api.Token.Refresh(_token.RefreshToken, _clientId, _clientSecret);
            Assert.NotNull(token);
            Console.WriteLine(token.AccessToken);
            Assert.NotEqual(token.AccessToken, _token.AccessToken);
        }
    }
}