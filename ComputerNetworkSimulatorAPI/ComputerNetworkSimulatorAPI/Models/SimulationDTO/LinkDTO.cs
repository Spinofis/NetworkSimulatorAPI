using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Models.SimulationDTO
{
    public class LinkDTO
    {
        public int Id { get; set; }
        public int IdSim { get; set; }
        public string NodeNumber1 { get; set; }
        public string NodeNumber2 { get; set; }
    }
}
