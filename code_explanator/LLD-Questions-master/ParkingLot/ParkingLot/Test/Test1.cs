using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ParkingLot.Entities;
using ParkingLot.Strategy;

namespace ParkingLot.Test
{
    public class Test1
    {
        Entities.ParkingLot ParkingLot;
        List<Vehicle> Vehicles;
        int cnt = 0;
        public Test1()
        {
            ParkingLot = Entities.ParkingLot.GetInstance();
            Vehicles = new List<Vehicle>();
            ParkingLot.ResetLevels();
            CreateLevels();
            CreateVehicles();
            StartParking();
        }

        private void CreateVehicles()
        {
            for(int i = 0; i < 20; i++)
            {
                Vehicles.Add(new Vehicle(i,i.ToString(),VehicleType.MotorCycle));
            }
            for (int i = 20; i < 40; i++)
            {
                Vehicles.Add(new Vehicle(i, i.ToString(), VehicleType.Car));
            }
            for (int i = 40; i < 60; i++)
            {
                Vehicles.Add(new Vehicle(i, i.ToString(), VehicleType.Truck));
            }
        }

        private void CreateLevels()
        {
            for (int i = 0; i < 10; i++)
            {
                ISearchAssignStrategy searchAssignStrategy = new LooseSearchAssign();
                Level level = new Level(searchAssignStrategy);
                searchAssignStrategy.AddLevel(level);
                ParkingLot.AddLevel(i, level);
                for (int j = 0; j < 20; j++)
                {
                    Slot slot = new Slot(j, VehicleType.MotorCycle);
                    level.AddSlot(slot);
                }
                for (int j = 20; j < 40; j++)
                {
                    Slot slot = new Slot(j, VehicleType.Car);
                    level.AddSlot(slot);
                }
                for (int j = 40; j < 60; j++)
                {
                    Slot slot = new Slot(j, VehicleType.Truck);
                    level.AddSlot(slot);
                }
                for (int j = 0; j < 4; j++)
                {
                    Gate gate = new Gate(i, searchAssignStrategy, level);
                    level.AddGate(gate);
                }
            }
        }
        private void StartParking()
        {
            Thread thread = new Thread(StartParking130);
            Thread thread1 = new Thread(StartParking3060);
            thread1.Start();
            thread.Start();
            thread1.Join();
            thread.Join();
            Console.WriteLine("asdfasdf");
            Console.WriteLine(cnt);
        }
        private void StartParking3060()
        {
            for(int i=0;i<30;i++)
            {
                if (ParkingLot.ParkVehicle(Vehicles[i])) Interlocked.Increment(ref cnt);
            }
        }
        private void StartParking130()
        {
            for (int i = 30; i < 60; i++)
            {
                if(ParkingLot.ParkVehicle(Vehicles[i])) Interlocked.Increment (ref cnt);
            }
        }
    }
}
