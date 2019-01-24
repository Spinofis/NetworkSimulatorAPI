using System;
using System.Collections.Generic;

namespace ComputerNetworkSimulatorAPI.Models.Database
{
    public partial class Links
    {
        public int Id { get; set; }
        public int? IdSim { get; set; }
        public string NodeNumber1 { get; set; }
        public string NodeNumber2 { get; set; }

        public Simulation IdSimNavigation { get; set; }
    }
}
