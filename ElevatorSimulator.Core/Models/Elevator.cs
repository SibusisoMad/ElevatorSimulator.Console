using ElevatorSimulator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Core.Models
{
    public class Elevator : IElevator
    {
        private readonly IElevatorController controller;
        private readonly int elevatorCapacity;
        private readonly List<int> passengers;
        private int currentFloor;
        private int targetFloor;

        public Elevator(IElevatorController controller, int elevatorCapacity)
        {
            this.controller = controller;
            this.elevatorCapacity = elevatorCapacity;
            passengers = new List<int>();
            currentFloor = 0;
            targetFloor = 0;
        }

        public int Id => controller.Elevators.IndexOf(this);

        public int CurrentFloor => currentFloor;

        public int NumPassengers => passengers.Count;

        public string Direction => currentFloor < targetFloor ? "Up" : "Down";

        public bool IsMoving => currentFloor != targetFloor;

        public bool IsAvailable => NumPassengers < elevatorCapacity;

        public int DistanceFrom(int floorNumber) => Math.Abs(currentFloor - floorNumber);

        public void GoTo(int floorNumber)
        {
            targetFloor = floorNumber;
        }

        public void Update()
        {
            if (currentFloor == targetFloor)
            {
                //check if elevator has passengers
                if (passengers.Count > 0)
                {
                    passengers.RemoveAll(p => p == currentFloor);
                    controller.Floors[currentFloor].NumWaiting += passengers.Count;
                }
                //check waiting passengers on currnt floor
                if (controller.Floors[currentFloor].NumWaiting > 0)
                {
                    var numAvailable = elevatorCapacity - passengers.Count;
                    var numToBoard = Math.Min(numAvailable, controller.Floors[currentFloor].NumWaiting);

                    passengers.AddRange(controller.Floors[currentFloor].BoardPassengers(numToBoard));
                }

                var target = -1;
                var minDistance = int.MaxValue;
                foreach (var floor in controller.Floors)
                {
                    if (floor.NumWaiting > 0)
                    {
                        var distance = DistanceFrom(floor.FloorNumber);

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            target = floor.FloorNumber;
                        }
                    }
                }

                if (target != -1)
                {
                    targetFloor = target;
                }
            }
            else
            {
                currentFloor += currentFloor < targetFloor ? 1 : -1;
            }
        }
    }
}
