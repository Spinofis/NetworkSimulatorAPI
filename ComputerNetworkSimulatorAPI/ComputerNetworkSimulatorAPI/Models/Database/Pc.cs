using System;
using System.Collections.Generic;

namespace ComputerNetworkSimulatorAPI.Models.Database
{
    public partial class Pc
    {
        public Pc()
        {
            PcSwitch = new HashSet<PcSwitch>();
        }

        public int Id { get; set; }
        public int? IdSim { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Mask { get; set; }

        public Simulation IdSimNavigation { get; set; }
        public ICollection<PcSwitch> PcSwitch { get; set; }
    }
}
