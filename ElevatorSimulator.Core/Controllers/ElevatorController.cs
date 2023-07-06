using ElevatorSimulator.Core.Interfaces;
using ElevatorSimulator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Core.Controllers
{
    public class ElevatorController : IElevatorController
    {
        private readonly int numFloors;
        private readonly int numElevators;
        private readonly int elevatorCapacity;
        private readonly List<IElevator> elevators;
        private readonly List<IFloor> floors;

        public ElevatorController(int numFloors, int numElevators, int elevatorCapacity)
        {
            this.numFloors = numFloors;
            this.numElevators = numElevators;
            this.elevatorCapacity = elevatorCapacity;

            elevators = new List<IElevator>();
            for (int i = 0; i < numElevators; i++)
            {
                elevators.Add(new Elevator(this, elevatorCapacity));
            }

            floors = new List<IFloor>();
            for (int i = 0; i < numFloors; i++)
            {
                floors.Add(new Floor(this, i));
            }
        }

        public List<IElevator> Elevators => elevators;

        public List<IFloor> Floors => floors;

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                foreach (var elevator in elevators)
                {
                    Console.WriteLine($"Elevator {elevator.Id} - Floor: {elevator.CurrentFloor}, " +
                        $"Direction: {elevator.Direction}, Moving: {elevator.IsMoving}, " +
                        $"Passengers: {elevator.NumPassengers}/{elevatorCapacity}");
                }
                Console.WriteLine();
                for (int i = 0; i < numFloors; i++)
                {
                    Console.WriteLine($"Floo {i} - Waiting: {floors[i].NumWaiting}");
                }

                Console.WriteLine("Enter command (e.g. call 2, 3): ");
                var input = Console.ReadLine().Split();
                if (input.Length == 2 && int.TryParse(input[1], out int floor))
                {
                    if (input[0] == "call")
                    {
                        floors[floor].CallElevator();
                    }
                    else if (input[0] == "set")
                    {
                        if (int.TryParse(input[2], out int numWaiting))
                        {
                            floors[floor].NumWaiting = numWaiting;
                        }
                    }
                }

                foreach (var elevator in elevators)
                {
                    elevator.Update();
                }
            }
        }
    }
}
