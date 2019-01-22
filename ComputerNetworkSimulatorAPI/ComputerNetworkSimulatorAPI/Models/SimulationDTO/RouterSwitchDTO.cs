using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Models.SimulationDTO
{
    public class RouterSwitchDTO
    {
        public RouterDTO Routers { get; set; }
        public SwitchDTO Switches { get; set; }
    }
}
