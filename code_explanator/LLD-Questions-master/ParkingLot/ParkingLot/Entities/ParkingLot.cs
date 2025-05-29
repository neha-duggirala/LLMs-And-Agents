using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Entities
{
    public class ParkingLot
    {
        private static ParkingLot instance;
        Dictionary<int, Level> levels = null;
        private ParkingLot()
        {
             levels = new Dictionary<int,Level>();

        }
        public static ParkingLot GetInstance()
        {
            if (instance == null)
            {
                lock (typeof(ParkingLot))
                {
                    if (instance == null)
                    {
                        instance = new ParkingLot();
                    }
                }
            }
            return instance;
        }
        public void AddLevel(int id, Level level) 
        {
            if (!levels.ContainsKey(id)) 
            {
                levels[id] = level;
            }
        }
        public void ResetLevels()
        {
            this.levels = new Dictionary<int, Level> { };
        }
        public bool ParkVehicle(Vehicle vehicle)
        {
            foreach (var level in levels.Values)
            {
                if(level.IsSlotAvailable(vehicle.GetType()))
                {
                    foreach(var gate in level.GetGates())
                    {
                        if (gate.ParkVehicle(vehicle)) return true;
                    }
                }
            }
            return false;
        }
    }
}
