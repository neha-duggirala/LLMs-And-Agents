using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Restaurant
{
    public class Restaurant
    {
        public string Name { get; set; }
        List<Branch> Branches;
        public Restaurant(string name)
        {
            this.Name = name;
        }
        public void AddBranch(Branch branch)
        {
            this.Branches.Add(branch);
        }
    }
}
