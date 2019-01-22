using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Models.SimulationDTO
{
    public class RouterDTO
    {
        public int Id { get; set; }
        public string NodeNumber { get; set; }
        public int RouterNumber { get; set; }
        public string Name { get; set; }
        public List<RouterInterfaceDTO> Interfaces { get; set; }
    }
}
