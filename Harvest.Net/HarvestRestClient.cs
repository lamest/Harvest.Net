using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Harvest.Net.Contracts;
using Harvest.Net.Resources;
using Harvest.Net.Utils;
using Newtonsoft.Json;
using Refit;

namespace Harvest.Net
{
    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly IAuthDataProvider _provider;

        public AuthenticatedHttpClientHandler(IAuthDataProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                var token = await _provider.GetToken().ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
            }

            if (request.Headers.Remove(Constants.AccountIdHeaderName))
            {
                var id = _provider.GetAccountId();
                request.Headers.Add(Constants.AccountIdHeaderName, id);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }

    public class HarvestRestClient : IHarvestRestClient, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _tokenHttpClient;

        public HarvestRestClient(IAuthDataProvider authDataProvider)
        {
            var refitSettings = new RefitSettings
            {
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new SnakeCaseContractResolver()
                }
            };

            HttpMessageHandler clientHandler = new AuthenticatedHttpClientHandler(authDataProvider);
#if DEBUG
            clientHandler = new HttpLoggingHandler(clientHandler);
#endif
            var httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri("https://api.harvestapp.com"),
                DefaultRequestHeaders =
                {
                    UserAgent =
                    {
                        new ProductInfoHeaderValue("harvest.net.fork", Assembly.GetExecutingAssembly().GetName().Version.ToString())
                    }
                }
            };
            var tokenHttpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri("https://id.getharvest.com"),
                DefaultRequestHeaders =
                {
                    UserAgent =
                    {
                        new ProductInfoHeaderValue("harvest.net.fork", Assembly.GetExecutingAssembly().GetName().Version.ToString())
                    }
                }
            };
            _tokenHttpClient = tokenHttpClient;
            _httpClient = httpClient;

            //_httpClient.DefaultRequestHeaders.Add("Harvest-Account-Id", accountId.ToString());

            Companies = RestService.For<ICompanyApi>(_httpClient, refitSettings);
            Clients = RestService.For<IClientApi>(_httpClient, refitSettings);
            Tasks = RestService.For<ITaskApi>(_httpClient, refitSettings);
            Timesheets = RestService.For<ITimesheetsApi>(_httpClient, refitSettings);
            Projects = RestService.For<IProjectsApi>(_httpClient, refitSettings);
            TaskAssignments = RestService.For<ITaskAssignmentsApi>(_httpClient, refitSettings);
            UserAssignments = RestService.For<IUserAssignmentsApi>(_httpClient, refitSettings);
            Token = RestService.For<IToken>(_tokenHttpClient, refitSettings);
            Account = RestService.For<IAccountsApi>(_tokenHttpClient, refitSettings);
        }

        public IAccountsApi Account { get; }

        public IToken Token { get; }

        public IUserAssignmentsApi UserAssignments { get; }

        public ITaskAssignmentsApi TaskAssignments { get; }

        public IProjectsApi Projects { get; }

        public ITaskApi Tasks { get; }

        public void Dispose()
        {
            _tokenHttpClient?.Dispose();
            _httpClient?.Dispose();
        }

        public IClientApi Clients { get; }

        public ICompanyApi Companies { get; }

        public ITimesheetsApi Timesheets { get; }
    }

    public interface IAuthDataProvider
    {
        Task<string> GetToken();

        string GetAccountId();
        //void Configure(string authorizationUri, string redirectUri, int clientId, string responseType);
    }
}