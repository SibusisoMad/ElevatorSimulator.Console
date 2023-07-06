using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Core.Interfaces
{
    public interface IElevator
    {
        int Id { get; }
        int CurrentFloor { get; }
        int NumPassengers { get; }

        string Direction { get; }

        bool IsMoving { get; }
        bool IsAvailable();
        int DistanceFrom(int floorNumber);
        void GoTo(int floorNumber);
        void Update();
    }
}
