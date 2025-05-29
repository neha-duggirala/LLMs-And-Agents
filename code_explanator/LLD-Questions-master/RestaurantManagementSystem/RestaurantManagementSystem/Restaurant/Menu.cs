using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Restaurant
{
    public class Menu
    {
        HashSet<MenuSection> menuSections;
    }
    public class MenuSection
    {
        HashSet<MenuItem> menuItems;
    }
    public class MenuItem
    {
        string name;
        int price;
        public MenuItem(string name, int price)
        {
            this.name = name;
            this.price = price;
        }
    }
}
