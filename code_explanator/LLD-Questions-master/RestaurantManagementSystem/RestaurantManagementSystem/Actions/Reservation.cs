using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystem.Roles;

namespace RestaurantManagementSystem.Actions
{
    public class Reservation
    {
        public ReservationStatus Status { get; set; }
        public DateTime BookedTime { get;}
        public Customer Customer { get;}
        public Order Order { get;}
        public Tabl

    }
    public enum ReservationStatus
    {
        Reserved,
        Checkedin,
        Cancled,
        UnReserved,
        OutOfService,
        Completed
    }
    public class Order
    {

    }
}
