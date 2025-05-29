using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorDesign.Controllers;
using ElevatorDesign.Strategy;

namespace ElevatorDesign.Entities
{
    public class ElevatorSystem
    {
        private static ElevatorSystem instance;
        private static readonly object _lock = new object();
        private List<Floor> floors;
        private List<ElevatorCar> elevatorCars;
        private FloorController floorController;
        private ElevatorSystem() { }
        public static ElevatorSystem GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ElevatorSystem();
                    }
                }
            }
            return instance;
        }
        public List<Floor> GetFloors()
        {
            return floors;
        }
        public List<ElevatorCar> GetElevatorCars()
        {
            return elevatorCars;
        }
        public void Init(int floorCount, int elevatorCarCount)
        {

            this.elevatorCars = new List<ElevatorCar>();
            this.floorController = new FloorController(elevatorCars);
            this.floors = new List<Floor>();
            for (int i = 1; i <= floorCount; i++)
            {
                floors.Add(new Floor(i, floorController));
            }
            for (int i = 1; i <= elevatorCarCount; i++)
            {
                IElevatorCarStrategy strategy = new AllFloorsElevatorStrategy();
                ElevatorCar car = new ElevatorCar(i, strategy);
                strategy.SetElevatorCar(car);
                elevatorCars.Add(car);

            }
        }
    }
}
