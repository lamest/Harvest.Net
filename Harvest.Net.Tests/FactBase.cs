using System.Configuration;
using System.Threading.Tasks;

namespace Harvest.Net.Tests
{
    public abstract class FactBase
    {
        public FactBase()
        {
            var idString = ConfigurationManager.AppSettings["MainAccountId"];
            UserId = int.Parse(idString);
            Token = ConfigurationManager.AppSettings["MainApiToken"];

            var provider = new TestAuthDataProvider(UserId.ToString(), Token);
            Api = new HarvestRestClient(provider);

            Initialize();
        }

        protected HarvestRestClient Api { get; set; }
        protected int UserId { get; }
        protected string Token { get; }

        protected int GetTestId(TestId key)
        {
            return int.Parse(ConfigurationManager.AppSettings["Test_" + key]);
        }

        /// <summary>
        ///     Initialize any necessary test items
        /// </summary>
        protected virtual void Initialize()
        {
        }

        protected enum TestId
        {
            ClientId,
            ContactId,
            ExpenseCategoryId,
            ExpenseId,
            InvoiceCategoryId,
            InvoiceId,
            PaymentId,
            ProjectId,
            ProjectId2,
            TaskAssignmentId,
            TaskId,
            TaskId2,
            UserAssignmentId,
            UserId,
            TimesheetId
        }
    }

    public class TestAuthDataProvider : IAuthDataProvider
    {
        private readonly string _token;
        private readonly string _userId;

        public TestAuthDataProvider(string userId, string token)
        {
            _userId = userId;
            _token = token;
        }

        public async Task<string> GetToken()
        {
            return _token;
        }

        public string GetAccountId()
        {
            return _userId;
        }
    }
}