using System;
using System.Collections.Generic;

namespace ComputerNetworkSimulatorAPI.Models.Database
{
    public partial class Switch
    {
        public int Id { get; set; }
        public int? IdSim { get; set; }
        public string NodeNumber { get; set; }
        public int? SwitchNumber { get; set; }
        public string Name { get; set; }
        public string HostIdentity { get; set; }

        public Simulation IdSimNavigation { get; set; }
    }
}
