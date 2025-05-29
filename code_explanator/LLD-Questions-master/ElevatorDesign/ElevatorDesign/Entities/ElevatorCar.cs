using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorDesign.Strategy;

namespace ElevatorDesign.Entities
{
    public class ElevatorCar
    {
        private CarDirection direction;
        List<CarButton> buttons;
        private IElevatorCarStrategy strategy;
        int CurrentFloor;
        public ElevatorCar(int floors,IElevatorCarStrategy elevatorCarStrategy) 
        {
            this.strategy = elevatorCarStrategy;
            this.CurrentFloor = 0;
            this.buttons = new List<CarButton>();
            direction = CarDirection.Neutral;
            for (int i = 1; i <= floors; i++) 
            {
                CarButton carButton = new CarButton(i);
                carButton.OnClick += strategy.GoToFloor;
                buttons.Add(carButton);
            }
        }
        public void ButtonClick(int floor,CarDirection carDirection)
        {
            foreach(CarButton carButton in buttons)
            {
                if(carButton != null&&carButton.GetFloorNumber()==floor)
                {
                    carButton.Click(carDirection);
                }
            }
        }
        public int GetCurrentFloor()
        {
            return CurrentFloor;
        }
        public CarDirection GetDirection() { return direction; }
        public void SetDirection(CarDirection direction) { this.direction = direction; }
    }
}
