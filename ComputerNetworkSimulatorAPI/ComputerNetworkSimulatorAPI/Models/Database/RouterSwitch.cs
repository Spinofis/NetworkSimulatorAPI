using System;
using System.Collections.Generic;

namespace ComputerNetworkSimulatorAPI.Models.Database
{
    public partial class RouterSwitch
    {
        public int Id { get; set; }
        public int IdRouter { get; set; }
        public int IdSwitch { get; set; }

        public Router IdRouterNavigation { get; set; }
        public Switch IdSwitchNavigation { get; set; }
    }
}
