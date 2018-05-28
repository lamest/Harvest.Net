using System.Threading.Tasks;
using Harvest.Net.Contracts;
using Harvest.Net.Models;
using Refit;

namespace Harvest.Net.Resources
{
    [Headers("Authorization: Bearer", Constants.AccountIdHeaderName + ": null")]
    public interface ICompanyApi
    {
        [Get("/v2/company")]
        Task<Company> GetAsync();
    }
}
