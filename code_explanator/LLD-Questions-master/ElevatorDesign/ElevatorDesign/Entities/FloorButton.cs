using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorDesign.Entities
{
    public class FloorButton : Button
    {
        public event Action<int, FloorButtonDirection> OnClick;
        private FloorButtonDirection direction;
        private int floorNumber=-1;
        public FloorButton(FloorButtonDirection floorButtonDirection,int floorNumber) 
        {
            this.direction = floorButtonDirection;
            this.floorNumber = floorNumber;
        }
        public void Click()
        {
            this.OnClick?.Invoke(floorNumber, direction);
        }
    }
}
