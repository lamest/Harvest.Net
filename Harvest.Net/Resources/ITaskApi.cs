using Harvest.Net.Containers;
using Harvest.Net.Models;
using Refit;
using System;
using System.Threading.Tasks;
using Harvest.Net.Contracts;

namespace Harvest.Net.Resources
{
    [Headers("Authorization: Bearer", Constants.AccountIdHeaderName + ": null")]
    public interface ITaskApi
    {
        [Get("/v2/tasks")]
        Task<HarvestTasksContainer> ListAll(
            [AliasAs("is_active")] bool? isActive = null,
            [AliasAs("updated_since")] DateTime? updatedSince = null,
            int? page = null,
            [AliasAs("per_page")] int? perPage = null
        );

        [Get("/v2/tasks/{id}")]
        Task<HarvestTask> Get(int id);

        [Post("/v2/tasks")]
        Task<HarvestTask> Add(
            string name,
            [AliasAs("billable_by_default")] bool? isBillableByDefault = null,
            [AliasAs("default_hourly_rate")] decimal? defaultHourlyRate = null,
            [AliasAs("is_default")] bool? isDefault = null,
            [AliasAs("is_active")] bool? isActive = null
            );

        [Patch("/v2/tasks/{id}")]
        Task<HarvestTask> Update(
            int id,
            string name = null,
            [AliasAs("billable_by_default")] bool? isBillableByDefault = null,
            [AliasAs("default_hourly_rate")] decimal? defaultHourlyRate = null,
            [AliasAs("is_default")] bool? isDefault = null,
            [AliasAs("is_active")] bool? isActive = null
            );

        [Delete("/v2/tasks/{id}")]
        Task Delete(int id);
    }
}
