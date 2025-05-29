using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingLot.Entities;

namespace ParkingLot.Strategy
{
    public interface ISearchAssignStrategy
    {
        bool IsSlotAvailable(VehicleType vehicleType);
        bool ParkVehicle(Vehicle vehicle);
        void AddLevel(Level level);
    }
}
