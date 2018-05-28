using Harvest.Net.Contracts;

namespace Harvest.Net.Models
{
    public class Account
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public HarvestApiProducts Products { get; set; }
    }
}