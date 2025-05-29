using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystem.Restaurant;

namespace RestaurantManagementSystem.Roles
{
    public class Receptionist
    {
        private Branch Branch;
        public Receptionist(Branch branch) 
        {
            this.Branch = branch;
        }
        public void MakeReservation(int tableId,Customer customer)
        {

        }
    }
}
