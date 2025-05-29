using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingLot.Strategy;

namespace ParkingLot.Entities
{
    public class Level
    {
        List<Slot> slots;
        List<Gate> gates;
        ISearchAssignStrategy searchAssignStrategy;
        public Level(ISearchAssignStrategy searchAssignStrategy)
        {
            this.searchAssignStrategy = searchAssignStrategy;
            this.slots = new List<Slot>();
            this.gates = new List<Gate>();
        }
        public bool IsSlotAvailable(VehicleType vehicleType) 
        {
            return searchAssignStrategy.IsSlotAvailable(vehicleType);
        }
        internal List<Slot> GetAvailableSlots() 
        {
            List<Slot> avlSlots = new List<Slot>();
            foreach (var slot in slots)
            {
                if (slot.IsSlotAvailable()) avlSlots.Add(slot);
            }
            return avlSlots;
        }
        public void AddSlot(Slot slot) 
        {
            if (slot == null) return;
            slots.Add(slot);
        }
        public void AddGate(Gate gate) { 
            gates.Add(gate);
        }
        public List<Gate> GetGates() 
        { 
            return gates;
        }
    }
}
