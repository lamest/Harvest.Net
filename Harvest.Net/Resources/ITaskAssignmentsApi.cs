using System;
using System.Threading.Tasks;
using Harvest.Net.Containers;
using Harvest.Net.Contracts;
using Harvest.Net.Models;
using Refit;

namespace Harvest.Net.Resources
{
    [Headers("Authorization: Bearer", Constants.AccountIdHeaderName + ": null")]
    public interface ITaskAssignmentsApi
    {
        /// <summary>
        ///     Returns a list of your task assignments for the project identified by PROJECT_ID.The task assignments are returned
        ///     sorted by creation date, with the most recently created task assignments appearing first.
        ///     The response contains an object with a task_assignments property that contains an array of up to per_page task
        ///     assignments.
        ///     Each entry in the array is a separate task assignment object. If no more task assignments are available, the
        ///     resulting array will be empty.
        ///     Several additional pagination properties are included in the response to simplify paginating your task assignments.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="isActive">Pass true to only return active task assignments and false to return inactive task assignments.</param>
        /// <param name="updatedSince">Only return task assignments that have been updated since the given date and time.</param>
        /// <param name="page">
        ///     The page number to use in pagination. For instance, if you make a list request and receive 100
        ///     records, your subsequent call can include page=2 to retrieve the next page of the list. (Default: 1)
        /// </param>
        /// <param name="perPage">The number of records to return per page. Can range between 1 and 100. (Default: 100)</param>
        /// <returns></returns>
        [Get("/v2/projects/{project_id}/task_assignments")]
        Task<TaskAssignmentsContainer> ListAll(
            [AliasAs("project_id")] int projectId,
            [AliasAs("is_active")] bool? isActive = null,
            [AliasAs("updated_since")] DateTime? updatedSince = null,
            int? page = null,
            [AliasAs("per_page")] int? perPage = null
        );

        /// <summary>
        ///     Retrieves the task assignment with the given ID. Returns a task assignment object and a 200 OK response code if a
        ///     valid identifier was provided.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Get("/v2/projects/{project_id}/task_assignments/{id}")]
        Task<TaskAssignment> Get(
            [AliasAs("project_id")] int projectId,
            int id);

        /// <summary>
        ///     Creates a new user assignment object. Returns a user assignment object and a 201 Created response code if the call
        ///     succeeded.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="taskId">The ID of the task to associate with the project.</param>
        /// <param name="isActive">Whether the task assignment is active or archived. Defaults to true.</param>
        /// <param name="billable">Whether the task assignment is billable or not. Defaults to false.</param>
        /// <param name="hourlyRate">
        ///     Rate used when the project’s bill_by is Tasks. Defaults to null when billing by task hourly
        ///     rate, otherwise 0.
        /// </param>
        /// <param name="budget">Budget used when the project’s budget_by is task or task_fees.</param>
        /// <returns></returns>
        [Post("/v2/projects/{project_id}/task_assignments")]
        Task<TaskAssignment> Add(
            [AliasAs("project_id")] int projectId,
            [AliasAs("task_id")] int taskId,
            [AliasAs("is_active")] bool? isActive = null,
            bool? billable = null,
            [AliasAs("hourly_rate")] decimal? hourlyRate = null,
            decimal? budget = null
        );


        /// <summary>
        ///     Updates the specific task assignment by setting the values of the parameters passed. Any parameters not provided
        ///     will be left unchanged. Returns a task assignment object and a 200 OK response code if the call succeeded.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <param name="isActive">Whether the task assignment is active or archived.</param>
        /// <param name="billable">Whether the task assignment is billable or not.</param>
        /// <param name="hourlyRate">Rate used when the project’s bill_by is Tasks.</param>
        /// <param name="budget">Budget used when the project’s budget_by is task or task_fees.</param>
        /// <returns></returns>
        [Patch("/v2/projects/{project_id}/task_assignments/{id}")]
        Task<TaskAssignment> Update(
            [AliasAs("project_id")] int projectId,
            int id,
            [AliasAs("is_active")] bool? isActive = null,
            bool? billable = null,
            [AliasAs("hourly_rate")] decimal? hourlyRate = null,
            decimal? budget = null
        );

        /// <summary>
        ///     Delete a task assignment. Deleting a task assignment is only possible if it has no time entries associated with it.
        ///     Returns a 200 OK response code if the call succeeded.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Delete("/v2/projects/{project_id}/task_assignments/{id}")]
        Task Delete(
            [AliasAs("project_id")] int projectId,
            int id
        );
    }
}