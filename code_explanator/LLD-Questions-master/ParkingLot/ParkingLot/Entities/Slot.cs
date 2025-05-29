using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Entities
{
    public class Slot
    {
        private VehicleType vehicleType;
        private readonly int slotId;
        private Vehicle vehicle;
        public SlotStatus slotStatus { get; set; }
        public Slot(int slotId, VehicleType vehicleType)
        {
            this.slotId = slotId;
            this.vehicleType = vehicleType;
            slotStatus = SlotStatus.Unreserved;
        }
        public void UpdateVehicleType(VehicleType vehicleType) 
        {
            this.vehicleType = vehicleType;
        }
        public bool IsSlotAvailable()
        {
            return slotStatus == SlotStatus.Unreserved;
        }
        public VehicleType GetVehicleType()
        {
            return vehicleType;
        }
        public bool ParkVehicle(Vehicle vehicle)
        {
            lock (this) // Locking on the specific Slot instance  
            {
                if (!IsSlotAvailable())
                {
                    return false;
                }
                this.vehicle = vehicle;
                this.slotStatus = SlotStatus.Reserved;
                Console.WriteLine(vehicle.name);
                return true;
            }
        }
        public void ReleaseVehicle()
        {
            if(vehicle == null||slotStatus == SlotStatus.Unreserved)
            {
                throw new Exception("Vehicle is not parked");
            }
            vehicle = null;
            slotStatus = SlotStatus.Unreserved;
        }


    }
}
