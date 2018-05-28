using System.Collections.Generic;
using Harvest.Net.Models;

namespace Harvest.Net.Containers
{
    public class AccountsContainer
    {
        public User User { get; set; }
        public List<Account> Accounts { get; set; }
    }
}