using System;
using System.Collections.Generic;

namespace ComputerNetworkSimulatorAPI.Models.Database
{
    public partial class Router
    {
        public Router()
        {
            RouterInterface = new HashSet<RouterInterface>();
            RouterSwitch = new HashSet<RouterSwitch>();
        }

        public int Id { get; set; }
        public int? IdSim { get; set; }
        public string NodeNumber { get; set; }
        public int? RouterNumber { get; set; }
        public string Name { get; set; }

        public Simulation IdSimNavigation { get; set; }
        public ICollection<RouterInterface> RouterInterface { get; set; }
        public ICollection<RouterSwitch> RouterSwitch { get; set; }
    }
}
