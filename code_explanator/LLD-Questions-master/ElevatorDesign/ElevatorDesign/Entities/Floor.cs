using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorDesign.Controllers;

namespace ElevatorDesign.Entities
{
    public class Floor
    {
        private int floorNumber { get; set; }
        private FloorButton upButton;
        private FloorButton downButton;
        private FloorController floorController;
        public Floor(int floorNumber, FloorController floorController)
        {
            this.floorNumber = floorNumber;
            this.floorController = floorController;
            upButton = new FloorButton(FloorButtonDirection.Up, floorNumber);
            downButton = new FloorButton(FloorButtonDirection.Down, floorNumber);
            upButton.OnClick += floorController.FloorButtonClick;
            downButton.OnClick += floorController.FloorButtonClick;
        }
        public void UpButtonClick()
        {
            this.upButton.Click();
        }
        public void DownButtonClick()
        {
            this.downButton.Click();
        }

    }
}
