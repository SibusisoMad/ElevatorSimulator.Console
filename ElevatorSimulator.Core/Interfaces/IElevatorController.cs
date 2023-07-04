using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator.Core.Interfaces
{
    public interface IElevatorController
    {
        List<IElevator> Elevators { get; }
        List<IFloor> Floors { get; }

        void Run();
    }
}
