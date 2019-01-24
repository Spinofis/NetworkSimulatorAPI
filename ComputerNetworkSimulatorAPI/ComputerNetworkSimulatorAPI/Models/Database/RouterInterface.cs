using System;
using System.Collections.Generic;

namespace ComputerNetworkSimulatorAPI.Models.Database
{
    public partial class RouterInterface
    {
        public int Id { get; set; }
        public int IdRouter { get; set; }
        public string Name { get; set; }
        public string IpHost { get; set; }
        public string IpNet { get; set; }
        public string Mask { get; set; }
        public string ConnectedNodeNumber { get; set; }

        public Router IdRouterNavigation { get; set; }
    }
}
