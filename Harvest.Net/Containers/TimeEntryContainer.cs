using Harvest.Net.Models;
using System.Collections.Generic;

namespace Harvest.Net.Containers
{
    public class TimeEntryContainer : ListContainerBase
    {
        public List<TimeEntry> TimeEntries { get; set; }
    }
}
