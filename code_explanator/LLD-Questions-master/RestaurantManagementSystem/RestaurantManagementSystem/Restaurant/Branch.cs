using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystem.Actions;
using RestaurantManagementSystem.Roles;

namespace RestaurantManagementSystem.Restaurant
{
    public class Branch
    {
        String name;
        String Address;
        List<Waiter> waiter;
        Receptionist receptionist;
        Menu menu;
        List<Table> tables;
        List<Reservation> reservations;

        public Branch(string name, string Address,int tableCount)
        {
            this.name = name;
            this.Address = Address;
            tables = new List<Table>();
            for (int i = 0; i < tableCount; i++) 
            {
                tables.Add(new Table(i));
            }
        }
        public void AddWaiter(Waiter waiter)
        {
            this.waiter.Add(waiter);
        }
        public void AddReceptionist(Receptionist receptionist)
        {
            this.receptionist = receptionist;
        }
    }
    public class Table
    {
        int id;
        public Table(int tableId)
        {
            this.id = tableId;
        }
    }
}
