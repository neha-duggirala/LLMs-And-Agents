using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorDesign.Entities;

namespace ElevatorDesign.Controllers
{
    public class FloorController
    {
        private List<ElevatorCar> cars;
        private static readonly object _lock = new object();
        public FloorController(List<ElevatorCar> cars) {
        this.cars = cars;
        }
        public void FloorButtonClick(int floorNumber, FloorButtonDirection direction)
        {
            lock (_lock) 
            {
                ElevatorCar sameDirectionCar = null;
                ElevatorCar idleCar = null;
                foreach(ElevatorCar car in cars)
                {
                    if(direction==FloorButtonDirection.Up&&car.GetDirection()==CarDirection.Up&&car.GetCurrentFloor()<floorNumber)
                    {
                        if (sameDirectionCar == null) { sameDirectionCar = car; }
                        else if(car.GetCurrentFloor()>sameDirectionCar.GetCurrentFloor()) 
                        {
                            sameDirectionCar = car;
                        }
                    }
                    if(direction==FloorButtonDirection.Down&&car.GetDirection()==CarDirection.Down&&car.GetCurrentFloor()>floorNumber)
                    {
                        if (sameDirectionCar == null) { sameDirectionCar = car; }
                        else if (car.GetCurrentFloor() < sameDirectionCar.GetCurrentFloor())
                        {
                            sameDirectionCar = car;
                        }
                    }
                    if(car.GetDirection()==CarDirection.Neutral)
                    {
                        if (idleCar == null) { idleCar = car; }
                        else if(Math.Abs(floorNumber-car.GetCurrentFloor())< Math.Abs(floorNumber - idleCar.GetCurrentFloor()))
                        {
                            idleCar = car;
                        }
                    }
                }
                if (sameDirectionCar != null)
                {
                    sameDirectionCar.ButtonClick(floorNumber,direction==FloorButtonDirection.Up?CarDirection.Up:CarDirection.Down);
                }
                else if (idleCar != null) 
                { 
                    idleCar.ButtonClick(floorNumber, direction == FloorButtonDirection.Up ? CarDirection.Up : CarDirection.Down);
                }
                else
                {
                    cars[0].ButtonClick(floorNumber, direction == FloorButtonDirection.Up ? CarDirection.Up : CarDirection.Down);
                }
            }

        }
    }
}
