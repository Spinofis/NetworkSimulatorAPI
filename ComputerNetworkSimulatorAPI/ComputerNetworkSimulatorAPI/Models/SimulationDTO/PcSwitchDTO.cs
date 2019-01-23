using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Models.SimulationDTO
{
    public class PcSwitchDTO
    {
        public PcDTO Pc { get; set; }
        public SwitchDTO Switch { get; set; }
    }
}
