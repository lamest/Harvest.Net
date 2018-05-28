using System.Collections.Generic;
using Harvest.Net.Models;

namespace Harvest.Net.Containers
{
    public class ProjectsContainer : ListContainerBase
    {
        public List<Project> Projects { get; set; }
    }
}