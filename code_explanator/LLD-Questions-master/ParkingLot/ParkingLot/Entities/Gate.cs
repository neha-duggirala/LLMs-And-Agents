using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingLot.Strategy;

namespace ParkingLot.Entities
{
    public class Gate
    {
        private int id;
        ISearchAssignStrategy SearchAssignStrategy;
        Level Level;
        public Gate(int id,ISearchAssignStrategy assignStrategy,Level level)
        {
            this.id = id;
            this.SearchAssignStrategy = assignStrategy;
            this.Level = level;
        }
        public bool ParkVehicle(Vehicle vehicle) 
        { 
            return SearchAssignStrategy.ParkVehicle(vehicle);
        }
        public void ExitVehicle(Slot slot)
        {
            slot.ReleaseVehicle();
        }
    }
}
