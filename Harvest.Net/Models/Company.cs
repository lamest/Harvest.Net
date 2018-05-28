using System;
using Harvest.Net.Enums;

namespace Harvest.Net.Models
{
    public class Company
    {
        /// <summary>
        ///     The Harvest URL for the company.
        /// </summary>
        public string BaseUri { get; set; }

        /// <summary>
        ///     The Harvest domain for the company.
        /// </summary>
        public string FullDomain { get; set; }

        /// <summary>
        ///     The name of the company.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Whether the company is active or archived.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        ///     The week day used as the start of the week. Returns one of: Saturday, Sunday, or Monday.
        /// </summary>
        public DayOfWeek WeekStartDay { get; set; }

        /// <summary>
        ///     Whether time is tracked via duration or start and end times.
        /// </summary>
        public bool WantsTimestampTimers { get; set; }

        /// <summary>
        ///     The format used to display time in Harvest. Returns either decimal or hours_minutes.
        /// </summary>
        public TimeFormat TimeFormat { get; set; }

        /// <summary>
        ///     The type of plan the company is on. Examples: trial, free, or simple-v4
        /// </summary>
        public string PlanType { get; set; }

        /// <summary>
        ///     Used to represent whether the company is using a 12-hour or 24-hour clock. Returns either 12h or 24h.
        /// </summary>
        public Clock Clock { get; set; }

        /// <summary>
        ///     Symbol used when formatting decimals.
        /// </summary>
        public string DecimalSymbol { get; set; }

        /// <summary>
        ///     Separator used when formatting numbers.
        /// </summary>
        public string ThousandsSeparator { get; set; }

        /// <summary>
        ///     The color scheme being used in the Harvest web client.
        /// </summary>
        public string ColorScheme { get; set; }

        /// <summary>
        ///     Whether the expense module is enabled.
        /// </summary>
        public bool ExpenseFeature { get; set; }

        /// <summary>
        ///     Whether the invoice module is enabled.
        /// </summary>
        public bool InvoiceFeature { get; set; }

        /// <summary>
        ///     Whether the estimate module is enabled.
        /// </summary>
        public bool EstimateFeature { get; set; }

        /// <summary>
        ///     Whether the approval module is enabled.
        /// </summary>
        public bool ApprovalFeature { get; set; }
    }
}