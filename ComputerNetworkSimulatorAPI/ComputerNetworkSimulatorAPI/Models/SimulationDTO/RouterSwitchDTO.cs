using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Models.SimulationDTO
{
    public class RouterSwitchDTO
    {
        public RouterDTO Router { get; set; }
        public SwitchDTO Switch { get; set; }
    }
}
