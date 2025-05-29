using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Entities
{
    public class Vehicle
    {
        private readonly int id;
        public readonly string name;
        private readonly VehicleType type;
        public Vehicle(int id, string name, VehicleType type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
        }
        public VehicleType GetType()
        {
            return type;
        }
        public string GetName()
        {
            return name;
        }

    }
}
