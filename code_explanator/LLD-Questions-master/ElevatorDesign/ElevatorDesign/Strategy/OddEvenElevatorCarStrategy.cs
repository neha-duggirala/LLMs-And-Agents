using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorDesign.Entities;

namespace ElevatorDesign.Strategy
{
    public class OddEvenElevatorCarStrategy : IElevatorCarStrategy
    {
        public void GoToFloor(int floor)
        {
            throw new NotImplementedException();
        }

        public void GoToFloor(int floor, CarDirection? carDirection)
        {
            throw new NotImplementedException();
        }

        public void SetElevatorCar(ElevatorCar car)
        {
            throw new NotImplementedException();
        }
    }
}
