using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystem.Restaurant;
using RestaurantManagementSystem.Roles;

namespace RestaurantManagementSystem.Tests
{
    public class Test1
    {
        RestaurantManagementSystem.Restaurant.Restaurant Restaurant;
        public Test1()
        {
            CreateRestaurant();
        }
        private void CreateRestaurant()
        {
            Restaurant = new RestaurantManagementSystem.Restaurant.Restaurant("Restaurant1");
            CreateBranch1();
            CreateBranch2();
        }
        private void CreateBranch1()
        {
            Branch branch = new Branch("branch1","hyderabad",10);
            Receptionist receptionist = new Receptionist(branch);
            branch.AddReceptionist(receptionist);
            for (int i = 0; i < 10; i++) {
                Waiter waiter = new Waiter("waiter1");
                branch.AddWaiter(waiter);
            }
            Restaurant.AddBranch(branch);
        }
        private void CreateBranch2() 
        {
            Branch branch = new Branch("branch2", "Bengaluru", 10);
            Receptionist receptionist = new Receptionist(branch);
            branch.AddReceptionist(receptionist);
            for (int i = 0; i < 10; i++)
            {
                Waiter waiter = new Waiter("waiter1");
                branch.AddWaiter(waiter);
            }
            Restaurant.AddBranch(branch);
        }
    }
}
