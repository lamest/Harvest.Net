using System;

namespace Harvest.Net.Models
{
    public class UserAssignment
    {
        /// <summary>
        ///     Unique ID for the user assignment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     An object containing the id and name of the associated user.
        /// </summary>
        public object User { get; set; }

        /// <summary>
        ///     Whether the user assignment is active or archived.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        ///     Determines if the user has project manager permissions for the project.
        /// </summary>
        public bool? IsProjectManager { get; set; }

        /// <summary>
        ///     Rate used when the project’s bill_by is People.
        /// </summary>
        public decimal? HourlyRate { get; set; }

        /// <summary>
        ///     Budget used when the project’s budget_by is person.
        /// </summary>
        public decimal? Budget { get; set; }

        /// <summary>
        ///     Date and time the user assignment was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        ///     Date and time the user assignment was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}