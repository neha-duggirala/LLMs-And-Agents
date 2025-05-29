using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Roles
{
    public class Customer
    {
        String Name { get; set; }
        List<Reservation> reservations;
        public Customer(string name)
        {
            this.Name = name;
            reservations = new List<Reservation>();
        }
        public void CancleReservation(Reservation reservation)
        {

        }
    }
}
