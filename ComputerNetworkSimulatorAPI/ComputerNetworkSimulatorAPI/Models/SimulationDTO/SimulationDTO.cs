using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Models.SimulationDTO
{
    public class SimulationDTO
    {
        public int Id { get; set; }
        public DateTime? DateAdd { get; set; }
        public DateTime? DateEdit { get; set; }
        public List<PcDTO> Pcs { get; set; }
        public List<RouterDTO> Routers { get; set; }
        public List<SwitchDTO> Switches { get; set; }
        //public List<RouterSwitchDTO> RouterSwitches { get; set; }
        //public List<PcSwitchDTO> PcSwitches { get; set; }
        public List<LinkDTO> Links { get; set; }
    }
}
