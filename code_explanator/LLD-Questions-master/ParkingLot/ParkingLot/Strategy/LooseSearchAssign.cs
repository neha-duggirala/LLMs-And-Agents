using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingLot.Entities;

namespace ParkingLot.Strategy
{
    public class LooseSearchAssign : ISearchAssignStrategy
    {
        private Level Level;
        public LooseSearchAssign() 
        {
        }
        public void AddLevel(Level level)
        {
            this.Level = level;
        }
        public bool ParkVehicle(Vehicle vehicle)
        {
            Slot slot = GetSlot(vehicle.GetType());
            if (slot.IsSlotAvailable())
            {
                if(slot.ParkVehicle(vehicle))
                return true;
            }
            return false;
        }
        private Slot GetSlot(VehicleType vehicleType)
        {
            List<Slot> avlSlots = Level.GetAvailableSlots();
            foreach (Slot slot in avlSlots)
            {
                if (vehicleType <= slot.GetVehicleType()) return slot;
            }
            return null;
        }

        public bool IsSlotAvailable(VehicleType vehicleType)
        {
            List<Slot> avlSlots = Level.GetAvailableSlots();
            foreach (Slot slot in avlSlots) 
            {
                if (vehicleType <= slot.GetVehicleType()) return true;
            }
            return false;
        }
    }
}
