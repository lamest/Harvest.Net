using System;
using System.Threading.Tasks;
using Harvest.Net.Containers;
using Harvest.Net.Contracts;
using Harvest.Net.Models;
using Refit;

namespace Harvest.Net.Resources
{
    [Headers("Authorization: Bearer", Constants.AccountIdHeaderName + ": null")]
    public interface IProjectsApi
    {
        /// <summary>
        ///     Returns a list of your projects. The projects are returned sorted by creation date, with the most recently created
        ///     projects appearing first.
        ///     The response contains an object with a projects property that contains an array of up to per_page projects.Each
        ///     entry in the array is a separate project object.
        ///     If no more projects are available, the resulting array will be empty. Several additional pagination properties are
        ///     included in the response to simplify
        ///     paginating your projects.
        /// </summary>
        /// <param name="isActive">Pass true to only return active projects and false to return inactive projects.</param>
        /// <param name="clientId">Only return projects belonging to the client with the given ID.</param>
        /// <param name="updatedSince">Only return projects that have been updated since the given date and time.</param>
        /// <param name="page">
        ///     The page number to use in pagination. For instance, if you make a list request and receive 100
        ///     records, your subsequent call can include page=2 to retrieve the next page of the list. (Default: 1)
        /// </param>
        /// <param name="perPage">The number of records to return per page. Can range between 1 and 100. (Default: 100)</param>
        /// <returns></returns>
        [Get("/v2/projects")]
        Task<ProjectsContainer> ListAll(
            [AliasAs("is_active")] bool? isActive = null,
            [AliasAs("client_id")] int? clientId = null,
            [AliasAs("updated_since")] DateTime? updatedSince = null,
            int? page = null,
            [AliasAs("per_page")] int? perPage = null
        );

        /// <summary>
        ///     Retrieves the project with the given ID. Returns a project object and a 200 OK response code if a valid identifier
        ///     was provided.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Get("/v2/projects/{id}")]
        Task<Project> Get(int id);

        /// <summary>
        ///     Creates a new project object. Returns a project object and a 201 Created response code if the call succeeded.
        /// </summary>
        /// <param name="clientId">The ID of the client to associate this project with.</param>
        /// <param name="name">The name of the project.</param>
        /// <param name="isBillable">Whether the project is billable or not.</param>
        /// <param name="billBy">The method by which the project is invoiced. Options: Project, Tasks, People, or none.</param>
        /// <param name="budgetBy">
        ///     The method by which the project is budgeted.
        ///     Options: project (Hours Per Project), project_cost (Total Project Fees), task (Hours Per Task), task_fees (Fees Per
        ///     Task), person (Hours Per Person), none (No Budget).
        /// </param>
        /// <param name="code">The code associated with the project.</param>
        /// <param name="isActive">Whether the project is active or archived. Defaults to true.</param>
        /// <param name="isFixedFee">Whether the project is a fixed-fee project or not.</param>
        /// <param name="hourlyRate">Rate for projects billed by Project Hourly Rate.</param>
        /// <param name="budget">The budget in hours for the project when budgeting by time.</param>
        /// <param name="notifyWhenOverBudget">
        ///     Whether project managers should be notified when the project goes over budget.
        ///     Defaults to false.
        /// </param>
        /// <param name="overBudgetNotificationPercentage">
        ///     Percentage value used to trigger over budget email alerts. Example: use
        ///     10.0 for 10.0%.
        /// </param>
        /// <param name="showBudgetToAll">
        ///     Option to show project budget to all employees. Does not apply to Total Project Fee
        ///     projects. Defaults to false.
        /// </param>
        /// <param name="costBudget">The monetary budget for the project when budgeting by money.</param>
        /// <param name="costBudgetIncludeExpenses">
        ///     Option for budget of Total Project Fees projects to include tracked expenses.
        ///     Defaults to false.
        /// </param>
        /// <param name="fee">The amount you plan to invoice for the project. Only used by fixed-fee projects.</param>
        /// <param name="notes">Project notes.</param>
        /// <param name="startsOn">Date the project was started.</param>
        /// <param name="endsOn">Date the project will end.</param>
        /// <returns></returns>
        [Post("/v2/projects")]
        Task<Project> Add(
            [AliasAs("client_id")] long clientId,
            string name,
            [AliasAs("is_billable")] bool isBillable,
            [AliasAs("bill_by")] string billBy,
            [AliasAs("budget_by")] string budgetBy,
            string code = null,
            [AliasAs("is_active")] bool? isActive = null,
            [AliasAs("is_fixed_fee")] bool? isFixedFee = null,
            [AliasAs("hourly_rate")] decimal? hourlyRate = null,
            decimal? budget = null,
            [AliasAs("notify_when_over_budget")] bool? notifyWhenOverBudget = null,
            [AliasAs("over_budget_notification_percentage")]
            decimal? overBudgetNotificationPercentage = null,
            [AliasAs("show_budget_to_all")] bool? showBudgetToAll = null,
            [AliasAs("cost_budget")] decimal? costBudget = null,
            [AliasAs("cost_budget_include_expenses")]
            decimal? costBudgetIncludeExpenses = null,
            decimal? fee = null,
            string notes = null,
            [AliasAs("starts_on")] DateTime? startsOn = null,
            [AliasAs("ends_on")] DateTime? endsOn = null
        );

        /// <summary>
        ///     Updates the specific project by setting the values of the parameters passed. Any parameters not provided will be
        ///     left unchanged.
        ///     Returns a project object and a 200 OK response code if the call succeeded.
        /// </summary>
        /// <param name="clientId">The ID of the client to associate this project with.</param>
        /// <param name="name">The name of the project.</param>
        /// <param name="isBillable">Whether the project is billable or not.</param>
        /// <param name="billBy">The method by which the project is invoiced. Options: Project, Tasks, People, or none.</param>
        /// <param name="budgetBy">
        ///     The method by which the project is budgeted.
        ///     Options: project (Hours Per Project), project_cost (Total Project Fees), task (Hours Per Task), task_fees (Fees Per
        ///     Task), person (Hours Per Person), none (No Budget).
        /// </param>
        /// <param name="code">The code associated with the project.</param>
        /// <param name="isActive">Whether the project is active or archived. Defaults to true.</param>
        /// <param name="isFixedFee">Whether the project is a fixed-fee project or not.</param>
        /// <param name="hourlyRate">Rate for projects billed by Project Hourly Rate.</param>
        /// <param name="budget">The budget in hours for the project when budgeting by time.</param>
        /// <param name="notifyWhenOverBudget">
        ///     Whether project managers should be notified when the project goes over budget.
        ///     Defaults to false.
        /// </param>
        /// <param name="overBudgetNotificationPercentage">
        ///     Percentage value used to trigger over budget email alerts. Example: use
        ///     10.0 for 10.0%.
        /// </param>
        /// <param name="showBudgetToAll">
        ///     Option to show project budget to all employees. Does not apply to Total Project Fee
        ///     projects. Defaults to false.
        /// </param>
        /// <param name="costBudget">The monetary budget for the project when budgeting by money.</param>
        /// <param name="costBudgetIncludeExpenses">
        ///     Option for budget of Total Project Fees projects to include tracked expenses.
        ///     Defaults to false.
        /// </param>
        /// <param name="fee">The amount you plan to invoice for the project. Only used by fixed-fee projects.</param>
        /// <param name="notes">Project notes.</param>
        /// <param name="startsOn">Date the project was started.</param>
        /// <param name="endsOn">Date the project will end.</param>
        /// <returns></returns>
        [Patch("/v2/projects/{id}")]
        Task<Project> Update(
            int id,
            [AliasAs("client_id")] int? clientId = null,
            string name = null,
            [AliasAs("is_billable")] bool? isBillable = null,
            [AliasAs("bill_by")] string billBy = null,
            [AliasAs("budget_by")] string budgetBy = null,
            string code = null,
            [AliasAs("is_active")] bool? isActive = null,
            [AliasAs("is_fixed_fee")] bool? isFixedFee = null,
            [AliasAs("hourly_rate")] decimal? hourlyRate = null,
            decimal? budget = null,
            [AliasAs("notify_when_over_budget")] bool? notifyWhenOverBudget = null,
            [AliasAs("over_budget_notification_percentage")]
            decimal? overBudgetNotificationPercentage = null,
            [AliasAs("show_budget_to_all")] bool? showBudgetToAll = null,
            [AliasAs("cost_budget")] decimal? costBudget = null,
            [AliasAs("cost_budget_include_expenses")]
            decimal? costBudgetIncludeExpenses = null,
            decimal? fee = null,
            string notes = null,
            [AliasAs("starts_on")] DateTime? startsOn = null,
            [AliasAs("ends_on")] DateTime? endsOn = null
        );

        /// <summary>
        ///     Deletes a project and any time entries or expenses tracked to it. However, invoices associated with the project
        ///     will not be deleted.
        ///     If you don’t want the project’s time entries and expenses to be deleted, you should archive the project instead.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Delete("/v2/projects/{id}")]
        Task Delete(int id);
    }
}