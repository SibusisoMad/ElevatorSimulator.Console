using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Core.Interfaces
{
    public interface IFloor
    {
        int NumWaiting { get; set; }
        int FloorNumber { get; }

        void CallElevator();

        List<int> BoardPassengers(int numPassengers);


    }
}
