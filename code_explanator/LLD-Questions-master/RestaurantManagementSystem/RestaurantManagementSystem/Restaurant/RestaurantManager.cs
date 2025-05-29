using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Restaurant
{
    public class RestaurantManager
    {
        private static RestaurantManager restaurantManager;
        private static Restaurant Restaurant = null;
        private RestaurantManager()
        {
            if (restaurantManager != null) throw new InvalidOperationException("Object already created");

        }
        public static RestaurantManager GetInstance(string restaurantName)
        {
            if (restaurantManager == null)
            {
                lock (typeof(RestaurantManager)) {
                    if (restaurantManager == null) 
                    { 
                        restaurantManager = new RestaurantManager();
                        Restaurant = new Restaurant(restaurantName);
                    }
                }
            }
            return restaurantManager;
        }
        public Restaurant GetRestaurant()
        {
            return Restaurant;
        }

    }
}
