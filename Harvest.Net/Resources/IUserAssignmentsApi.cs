using System;
using System.Threading.Tasks;
using Harvest.Net.Containers;
using Harvest.Net.Contracts;
using Harvest.Net.Models;
using Refit;

namespace Harvest.Net.Resources
{
    [Headers("Authorization: Bearer", Constants.AccountIdHeaderName + ": null")]
    public interface IUserAssignmentsApi
    {
        /// <summary>
        ///     Returns a list of your user assignments for the project identified by PROJECT_ID. The user assignments are returned
        ///     sorted by creation date, with the most recently created user assignments appearing first.
        ///     The response contains an object with a user_assignments property that contains an array of up to per_page user
        ///     assignments.
        ///     Each entry in the array is a separate user assignment object. If no more user assignments are available, the
        ///     resulting array will be empty.
        ///     Several additional pagination properties are included in the response to simplify paginating your user assignments.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="isActive">Pass true to only return active user assignments and false to return inactive user assignments.</param>
        /// <param name="updatedSince">Only return user assignments that have been updated since the given date and time.</param>
        /// <param name="page">
        ///     The page number to use in pagination. For instance, if you make a list request and receive 100
        ///     records, your subsequent call can include page=2 to retrieve the next page of the list. (Default: 1)
        /// </param>
        /// <param name="perPage">The number of records to return per page. Can range between 1 and 100. (Default: 100)</param>
        /// <returns></returns>
        [Get("/v2/projects/{project_id}/user_assignments")]
        Task<UserAssignmentsContainer> ListAll(
            [AliasAs("project_id")] int projectId,
            [AliasAs("is_active")] bool? isActive = null,
            [AliasAs("updated_since")] DateTime? updatedSince = null,
            int? page = null,
            [AliasAs("per_page")] int? perPage = null
        );

        /// <summary>
        ///     Retrieves the user assignment with the given ID. Returns a user assignment object and a 200 OK response code if a
        ///     valid identifier was provided.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Get("/v2/projects/{project_id}/user_assignments/{id}")]
        Task<UserAssignment> Get(
            [AliasAs("project_id")] int projectId,
            int id);

        /// <summary>
        ///     Creates a new user assignment object. Returns a user assignment object and a 201 Created response code if the call
        ///     succeeded.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId">The ID of the user to associate with the project.</param>
        /// <param name="isActive">Whether the user assignment is active or archived. Defaults to true.</param>
        /// <param name="isProjectManager">
        ///     Determines if the user has project manager permissions for the project. Defaults to
        ///     false for users with Regular User permissions and true for those with Project Managers or Administrator
        ///     permissions.
        /// </param>
        /// <param name="hourlyRate">Rate used when the project’s bill_by is People. Defaults to 0.</param>
        /// <param name="budget">Budget used when the project’s budget_by is person.</param>
        /// <returns></returns>
        [Post("/v2/projects/{project_id}/user_assignments")]
        Task<UserAssignment> Add(
            [AliasAs("project_id")] int projectId,
            [AliasAs("user_id")] int userId,
            [AliasAs("is_active")] bool? isActive = null,
            [AliasAs("is_project_manager")] bool? isProjectManager = null,
            [AliasAs("hourly_rate")] decimal? hourlyRate = null,
            decimal? budget = null
        );

        /// <summary>
        ///     Updates the specific user assignment by setting the values of the parameters passed. Any parameters not provided
        ///     will be left unchanged. Returns a user assignment object and a 200 OK response code if the call succeeded.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <param name="isActive">Whether the user assignment is active or archived. Defaults to true.</param>
        /// <param name="isProjectManager">
        ///     Determines if the user has project manager permissions for the project. Defaults to
        ///     false for users with Regular User permissions and true for those with Project Managers or Administrator
        ///     permissions.
        /// </param>
        /// <param name="hourlyRate">Rate used when the project’s bill_by is People. Defaults to 0.</param>
        /// <param name="budget">Budget used when the project’s budget_by is person.</param>
        /// <returns></returns>
        [Patch("/v2/projects/{project_id}/user_assignments/{id}")]
        Task<UserAssignment> Update(
            [AliasAs("project_id")] int projectId,
            int id,
            [AliasAs("is_active")] bool? isActive = null,
            [AliasAs("is_project_manager")] bool? isProjectManager = null,
            [AliasAs("hourly_rate")] decimal? hourlyRate = null,
            decimal? budget = null
        );

        /// <summary>
        ///     Delete a user assignment. Deleting a user assignment is only possible if it has no time entries or expenses
        ///     associated with it. Returns a 200 OK response code if the call succeeded.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Delete("/v2/projects/{project_id}/user_assignments/{id}")]
        Task Delete(
            [AliasAs("project_id")] int projectId,
            int id
        );
    }
}