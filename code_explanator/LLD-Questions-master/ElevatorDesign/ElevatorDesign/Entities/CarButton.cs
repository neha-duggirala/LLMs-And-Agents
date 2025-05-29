using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDesign.Entities
{
    public class CarButton : Button
    {
        public event Action<int,CarDirection?> OnClick;
        int number;
        public CarButton(int number)
        {
            this.number = number;
        }
        public int GetFloorNumber()
        {
            return number;
        }
        public void Click(CarDirection? direction)
        {
            OnClick?.Invoke(number,direction);
        }
    }
}
