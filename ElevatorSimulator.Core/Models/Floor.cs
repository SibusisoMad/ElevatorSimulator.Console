using ElevatorSimulator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Core.Models
{
    public  class Floor : IFloor
    {
        private readonly IElevatorController controller;
        private readonly List<int> waitingPassengers;

        public Floor(IElevatorController controller, int floorNumber)
        {
            this.controller = controller;
            FloorNumber = floorNumber;
            waitingPassengers = new List<int>();
        }

        public int NumWaiting
        {
            get => waitingPassengers.Count;

            set
            {
                if (value >= 0)
                {
                    waitingPassengers.Clear();
                    for (int i = 0; i < value; i++) ;
                    {
                        waitingPassengers.Add(FloorNumber);
                    }
                }
            }
        }
         public int FloorNumber { get; }

        public void CallElevator()
        {
            var bestEvelator = -1;
            var minDistance = int.MaxValue;
            foreach ( var elevator in controller.Elevators)
            {
                if (elevator.IsAvailable())
                {
                    var distance = elevator.DistanceFrom(FloorNumber);
                    
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        bestEvelator = elevator.Id;
                    }
                }
            }

            if (bestEvelator != -1)
            {
                controller.Elevators[bestEvelator].GoTo(FloorNumber);
            }
        }

        public List<int> BoardPassengers(int numberPassengers)
        {
            var toBoard = new List<int>();
            for (int i = 0; i < numberPassengers && i < waitingPassengers.Count; i++)
            {
                toBoard.Add(waitingPassengers[i]);
            }

            waitingPassengers.RemoveRange(0, toBoard.Count);
            return toBoard;
        }
    }
}
