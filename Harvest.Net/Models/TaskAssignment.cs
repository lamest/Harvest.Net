using System;

namespace Harvest.Net.Models
{
    public class TaskAssignment
    {
        /// <summary>
        ///     Unique ID for the task assignment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     An object containing the id and name of the associated task.
        /// </summary>
        public HarvestTask Task { get; set; }

        /// <summary>
        ///     Whether the task assignment is active or archived.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        ///     Whether the task assignment is billable or not. For example: if set to true, all time tracked on this project for
        ///     the associated task will be marked as billable.
        /// </summary>
        public bool? Billable { get; set; }

        /// <summary>
        ///     Rate used when the project’s bill_by is Tasks.
        /// </summary>
        public decimal? HourlyRate { get; set; }

        /// <summary>
        ///     Budget used when the project’s budget_by is task or task_fees.
        /// </summary>
        public decimal? Budget { get; set; }

        /// <summary>
        ///     Date and time the task assignment was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        ///     Date and time the task assignment was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}