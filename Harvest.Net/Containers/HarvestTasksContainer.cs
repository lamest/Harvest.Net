using Harvest.Net.Models;
using System.Collections.Generic;

namespace Harvest.Net.Containers
{
    public class HarvestTasksContainer: ListContainerBase
    {
        public List<HarvestTask> Tasks { get; set; }
    }
}
