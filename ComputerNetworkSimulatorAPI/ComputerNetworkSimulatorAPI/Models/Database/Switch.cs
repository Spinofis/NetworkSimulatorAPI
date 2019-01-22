using System;
using System.Collections.Generic;

namespace ComputerNetworkSimulatorAPI.Models.Database
{
    public partial class Switch
    {
        public Switch()
        {
            PcSwitch = new HashSet<PcSwitch>();
            RouterSwitch = new HashSet<RouterSwitch>();
        }

        public int Id { get; set; }
        public int? IdSim { get; set; }
        public string Name { get; set; }

        public Simulation IdSimNavigation { get; set; }
        public ICollection<PcSwitch> PcSwitch { get; set; }
        public ICollection<RouterSwitch> RouterSwitch { get; set; }
    }
}
