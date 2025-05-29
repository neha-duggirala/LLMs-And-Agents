using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorDesign.Entities;

namespace ElevatorDesign.Strategy
{
    public interface IElevatorCarStrategy
    {
        public void GoToFloor(int floor,CarDirection? carDirection);
        public void SetElevatorCar(ElevatorCar car);
    }
}
