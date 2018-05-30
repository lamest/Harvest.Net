using System;
using System.Threading.Tasks;
using Harvest.Net.Containers;
using Harvest.Net.Contracts;
using Harvest.Net.Models;
using Harvest.Net.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Harvest.Net.Resources
{
    [Headers("Authorization: Bearer", Constants.AccountIdHeaderName + ": null")]
    public interface ITimesheetsApi
    {
        /// <summary>
        ///     Returns a list of your time entries. The time entries are returned sorted by creation date, with the most recently
        ///     created time entries appearing first.
        ///     The response contains an object with a time_entries property that contains an array of up to per_page time
        ///     entries.Each entry in the array is a separate time entry object.
        ///     If no more time entries are available, the resulting array will be empty. Several additional pagination properties
        ///     are included in the response to simplify paginating your time entries.
        /// </summary>
        /// <param name="userId">Only return time entries belonging to the user with the given ID.</param>
        /// <param name="clientId">Only return time entries belonging to the client with the given ID.</param>
        /// <param name="projectId">Only return time entries belonging to the project with the given ID.</param>
        /// <param name="isBilled">
        ///     Pass true to only return time entries that have been invoiced and false to return time entries
        ///     that have not been invoiced.
        /// </param>
        /// <param name="isRunning">Pass true to only return running time entries and false to return non-running time entries.</param>
        /// <param name="updatedSince">Only return time entries that have been updated since the given date and time.</param>
        /// <param name="from">Only return time entries with a spent_date on or after the given date.</param>
        /// <param name="to">Only return time entries with a spent_date on or before the given date.</param>
        /// <param name="page">
        ///     The page number to use in pagination. For instance, if you make a list request and receive 100
        ///     records, your subsequent call can include page=2 to retrieve the next page of the list. (Default: 1)
        /// </param>
        /// <param name="perPage">The number of records to return per page. Can range between 1 and 100. (Default: 100)</param>
        /// <returns></returns>
        [Get("/v2/time_entries")]
        Task<TimeEntryContainer> ListAll(
            [AliasAs("user_id")] int? userId = null,
            [AliasAs("client_id")] int? clientId = null,
            [AliasAs("project_id")] int? projectId = null,
            [AliasAs("is_billed")] bool? isBilled = null,
            [AliasAs("is_running")] bool? isRunning = null,
            [AliasAs("updated_since")] DateTime? updatedSince = null,
            [Query(Format = "s")] DateTime? from = null,
            [Query(Format = "s")] DateTime? to = null,
            int? page = null,
            [AliasAs("per_page")] int? perPage = null
        );

        /// <summary>
        ///     Retrieves the time entry with the given ID. Returns a time entry object and a 200 OK response code if a valid
        ///     identifier was provided.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Get("/v2/time_entries/{id}")]
        Task<TimeEntry> Get(int id);

        /// <summary>
        ///     Creates a new time entry object. Returns a time entry object and a 201 Created response code if the call succeeded.
        ///     You should only use this method to create time entries when your account is configured to track time via duration.
        ///     You can verify this by visiting the Settings page in your Harvest account or by checking if wants_timestamp_timers
        ///     is false in the Company API.
        /// </summary>
        /// <param name="projectId">The ID of the project to associate with the time entry.</param>
        /// <param name="taskId">The ID of the task to associate with the time entry.</param>
        /// <param name="spentDate">The ISO 8601 formatted date the time entry was spent.</param>
        /// <param name="userId">
        ///     The ID of the user to associate with the time entry. Defaults to the currently authenticated
        ///     user’s ID.
        /// </param>
        /// <param name="hours">
        ///     The current amount of time tracked. If provided, the time entry will be created with the specified
        ///     hours and is_running will be set to false. If not provided, hours will be set to 0.0 and is_running will be set to
        ///     true.
        /// </param>
        /// <param name="notes">Any notes to be associated with the time entry.</param>
        /// <param name="externalReference">An object containing the id, group_id, and permalink of the external reference.</param>
        /// <returns></returns>
        [Post("/v2/time_entries")]
        Task<TimeEntry> AddViaDuration(
            [AliasAs("project_id")] int projectId,
            [AliasAs("task_id")] int taskId,
            [AliasAs("spent_date"), Query(Format = "s")] DateTime spentDate,
            [AliasAs("user_id")] int? userId = null,
            decimal? hours = null,
            string notes = null,
            [AliasAs("external_reference")] object externalReference = null
        );

        /// <summary>
        ///     Creates a new time entry object. Returns a time entry object and a 201 Created response code if the call succeeded.
        ///     You should only use this method to create time entries when your account is configured to track time via start and
        ///     end time.
        ///     You can verify this by visiting the Settings page in your Harvest account or by checking if wants_timestamp_timers
        ///     is true in the Company API.
        /// </summary>
        /// <param name="projectId">The ID of the project to associate with the time entry.</param>
        /// <param name="taskId">The ID of the task to associate with the time entry.</param>
        /// <param name="spentDate">The ISO 8601 formatted date the time entry was spent.</param>
        /// <param name="userId">
        ///     The ID of the user to associate with the time entry. Defaults to the currently authenticated
        ///     user’s ID.
        /// </param>
        /// <param name="startedTime">The time the entry started. Defaults to the current time. Example: “8:00am”.</param>
        /// <param name="startedTime">
        ///     The time the entry ended. If provided, is_running will be set to false. If not provided,
        ///     is_running will be set to true.
        /// </param>
        /// <param name="notes">Any notes to be associated with the time entry.</param>
        /// <param name="externalReference">An object containing the id, group_id, and permalink of the external reference.</param>
        /// <returns></returns>
        [Post("/v2/time_entries")]
        Task<TimeEntry> AddViaStartAndEndTime(
            [AliasAs("project_id")] int projectId,
            [AliasAs("task_id")] int taskId,
            [AliasAs("spent_date"), Query(Format = "s")] DateTime spentDate,
            [AliasAs("user_id")] int? userId = null,
            [AliasAs("started_time")] string startedTime = null,
            [AliasAs("ended_time")] string endedTime = null,
            string notes = null,
            [AliasAs("external_reference")] object externalReference = null
        );


        /// <summary>
        ///     Updates the specific time entry by setting the values of the parameters passed. Any parameters not provided will be
        ///     left unchanged.
        ///     Returns a time entry object and a 200 OK response code if the call succeeded.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pojectId">The ID of the project to associate with the time entry.</param>
        /// <param name="taskId">The ID of the task to associate with the time entry.</param>
        /// <param name="spentDate">The ISO 8601 formatted date the time entry was spent.</param>
        /// <param name="startedTime">The time the entry started. Defaults to the current time. Example: “8:00am”.</param>
        /// <param name="endedTime">The time the entry ended.</param>
        /// <param name="hours">The current amount of time tracked.</param>
        /// <param name="notes">Any notes to be associated with the time entry.</param>
        /// <param name="externalReference">An object containing the id, group_id, and permalink of the external reference.</param>
        /// <returns></returns>
        [Patch("/v2/time_entries/{id}")]
        Task<TimeEntry> Update(
            int id,
            [AliasAs("project_id")] int? pojectId = null,
            [AliasAs("task_id")] int? taskId = null,
            [AliasAs("spent_date"), Query(Format = "s")] DateTime? spentDate = null,
            [AliasAs("started_time")] TimeSpan? startedTime = null,
            TimeSpan? endedTime = null,
            decimal? hours = null,
            string notes = null,
            [AliasAs("external_reference")] object externalReference = null
        );

        /// <summary>
        ///     Delete a time entry. Deleting a time entry is only possible if it’s not closed and the associated project and task
        ///     haven’t been archived.
        ///     However, Admins can delete closed entries. Returns a 200 OK response code if the call succeeded.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Delete("/v2/time_entries/{id}")]
        Task Delete(int id);

        /// <summary>
        ///     Restarting a time entry is only possible if it isn’t currently running. Returns a 200 OK response code if the call
        ///     succeeded.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Patch("/v2/time_entries/{id}/restart")]
        Task<TimeEntry> Restart(int id);

        [Patch("/v2/time_entries/{id}/stop")]
        Task<TimeEntry> Stop(int id);
    }
}