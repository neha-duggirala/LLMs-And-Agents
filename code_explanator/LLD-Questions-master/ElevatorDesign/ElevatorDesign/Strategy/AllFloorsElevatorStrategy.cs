using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElevatorDesign.Entities;

namespace ElevatorDesign.Strategy
{
    public class AllFloorsElevatorStrategy : IElevatorCarStrategy
    {
        private ElevatorCar _elevatorCar;
        private readonly List<int> _upPendingJobs = new List<int>();
        private readonly List<int> _downPendingJobs = new List<int>();
        private readonly PriorityQueue<int, int> _up = new PriorityQueue<int, int>();
        private readonly PriorityQueue<int, int> _down = new PriorityQueue<int, int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
        private int _processing = 0; // 0 = not processing, 1 = processing
        private readonly object _lock = new object();

        public void GoToFloor(int floor, CarDirection? goToDirection)
        {
            lock (_lock)
            {
                if (_elevatorCar == null)
                    throw new InvalidOperationException("Elevator car not set.");

                // If goToDirection is null, it's an internal request
                if (goToDirection == null)
                {
                    var currentFloor = _elevatorCar.GetCurrentFloor();
                    var direction = _elevatorCar.GetDirection();
                    if (direction == CarDirection.Up)
                        goToDirection = floor > currentFloor ? CarDirection.Up : CarDirection.Down;
                    else if (direction == CarDirection.Down)
                        goToDirection = floor < currentFloor ? CarDirection.Down : CarDirection.Up;
                    else
                        goToDirection = floor >= currentFloor ? CarDirection.Up : CarDirection.Down;
                }

                if (goToDirection == CarDirection.Up)
                {
                    if ((_up.Count == 0 || floor >= _up.Peek() || _elevatorCar.GetDirection() == CarDirection.Down)
                        && !_up.UnorderedItems.Any(x => x.Element == floor)
                        && !_upPendingJobs.Contains(floor))
                    {
                        _up.Enqueue(floor, floor);
                    }
                    else if (!_upPendingJobs.Contains(floor))
                    {
                        _upPendingJobs.Add(floor);
                    }
                }
                else if (goToDirection == CarDirection.Down)
                {
                    if ((_down.Count == 0 || floor <= _down.Peek() || _elevatorCar.GetDirection() == CarDirection.Up)
                        && !_down.UnorderedItems.Any(x => x.Element == floor)
                        && !_downPendingJobs.Contains(floor))
                    {
                        _down.Enqueue(floor, floor);
                    }
                    else if (!_downPendingJobs.Contains(floor))
                    {
                        _downPendingJobs.Add(floor);
                    }
                }

                // Start processing in a non-blocking way
                Task.Run(() => StartProcessing());
            }
        }

        private void StartProcessing()
        {
            // Only one thread can process at a time
            if (Interlocked.CompareExchange(ref _processing, 1, 0) != 0)
                return;

            try
            {
                if (_elevatorCar == null)
                    throw new InvalidOperationException("Elevator car not set.");

                while (true)
                {
                    PriorityQueue<int, int> currPQ;
                    PriorityQueue<int, int> nextPQ;
                    List<int> currPendingJobs;

                    if (_elevatorCar.GetDirection() == CarDirection.Up)
                    {
                        currPQ = _up;
                        currPendingJobs = _upPendingJobs;
                        nextPQ = _down;
                    }
                    else if (_elevatorCar.GetDirection() == CarDirection.Down)
                    {
                        currPQ = _down;
                        currPendingJobs = _downPendingJobs;
                        nextPQ = _up;
                    }
                    else
                    {
                        // Decide direction if neutral and jobs exist
                        if (_up.Count > 0)
                        {
                            _elevatorCar.SetDirection(CarDirection.Up);
                            currPQ = _up;
                            currPendingJobs = _upPendingJobs;
                            nextPQ = _down;
                        }
                        else if (_down.Count > 0)
                        {
                            _elevatorCar.SetDirection(CarDirection.Down);
                            currPQ = _down;
                            currPendingJobs = _downPendingJobs;
                            nextPQ = _up;
                        }
                        else
                        {
                            break; // No jobs to process
                        }
                    }

                    while (currPQ.Count > 0)
                    {
                        int nextFloor = currPQ.Peek();
                        Console.WriteLine($"Going {_elevatorCar.GetDirection()} to {nextFloor}");
                        currPQ.Dequeue();

                        // Simulate travel delay (optional)
                        // await Task.Delay(200);
                    }

                    // Move pending jobs into the current queue
                    if (currPendingJobs.Count > 0)
                    {
                        foreach (int floor in currPendingJobs)
                        {
                            if (!currPQ.UnorderedItems.Any(x => x.Element == floor))
                                currPQ.Enqueue(floor, floor);
                        }
                        currPendingJobs.Clear();
                        continue; // Process new jobs just added
                    }

                    // Switch direction if next queue has jobs
                    if (nextPQ.Count > 0)
                    {
                        _elevatorCar.SetDirection(_elevatorCar.GetDirection() == CarDirection.Up ? CarDirection.Down : CarDirection.Up);
                        continue;
                    }
                    else
                    {
                        _elevatorCar.SetDirection(CarDirection.Neutral);
                        break;
                    }
                }
            }
            finally
            {
                Interlocked.Exchange(ref _processing, 0);
            }
        }

        public void SetElevatorCar(ElevatorCar car)
        {
            _elevatorCar = car ?? throw new ArgumentNullException(nameof(car));
        }
    }
}

