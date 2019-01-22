using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Models.SimulationDTO
{
    public class RouterInterfaceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpHost { get; set; }
        public string IpNet {get;set;}
        public string Mask { get; set; }

    }
}
