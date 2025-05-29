using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ElevatorDesign.Entities;
using ElevatorDesign.Strategy;

namespace ElevatorDesign.Tests
{
    public class Test1
    {
        private readonly List<Floor> floors;
        private readonly List<ElevatorCar> cars;
        private readonly Random random = new Random();
        private readonly int iterations = 50; // Number of actions per thread

        public Test1()
        {
            ElevatorSystem elevatorSystem = ElevatorSystem.GetInstance();
            elevatorSystem.Init(20, 6);
            floors = elevatorSystem.GetFloors();
            cars = elevatorSystem.GetElevatorCars();

            Thread thread1 = new Thread(() => SimulateCarAndFloorActions(odd: false)); // Even cars: 0,2,4
            Thread thread2 = new Thread(() => SimulateCarAndFloorActions(odd: true));  // Odd cars: 1,3,5

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }

        private void SimulateCarAndFloorActions(bool odd)
        {
            // Select car indices based on thread
            var carIndices = Enumerable.Range(0, cars.Count)
                                       .Where(i => (i % 2 == 1) == odd)
                                       .ToList();

            for (int i = 0; i < iterations; i++)
            {
                // Randomly select a car
                int carIdx = carIndices[random.Next(carIndices.Count)];
                ElevatorCar car = cars[carIdx];

                // Randomly select a floor to press inside the car
                int targetFloor = random.Next(floors.Count);
                CarDirection direction = (CarDirection)random.Next(0, 3); // Up, Down, Neutral

                car.ButtonClick(targetFloor, direction);

                // Randomly select a floor and press up/down button
                int floorIdx = random.Next(floors.Count);
                Floor floor = floors[floorIdx];

                // Randomly choose up or down button, but avoid invalid presses (e.g., up on top floor)
                bool pressUp = random.Next(2) == 0;
                if (pressUp && floorIdx < floors.Count - 1)
                {
                    floor.UpButtonClick();
                }
                else if (!pressUp && floorIdx > 0)
                {
                    floor.DownButtonClick();
                }

                Thread.Sleep(50); // Simulate time between actions
            }
        }
    }
}