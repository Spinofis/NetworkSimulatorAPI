using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Models.SimulationDTO
{
    public class SwitchDTO
    {
        public int Id { get; set; }
        public string NodeNumber { get; set; }
        public int SwitchNumber { get; set; }
        public string Name { get; set; }
    }
}
