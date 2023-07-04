using System;
using ElevatorSimulator.Core;

namespace ElevatorSimulator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new ElevatorController(numFloors: 10, numElevators: 3, elevatorCapacity: 10);
            controller.Run();
        }
    }
}
