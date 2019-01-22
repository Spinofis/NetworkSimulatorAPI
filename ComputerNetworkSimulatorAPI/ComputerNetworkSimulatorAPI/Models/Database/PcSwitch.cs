using System;
using System.Collections.Generic;

namespace ComputerNetworkSimulatorAPI.Models.Database
{
    public partial class PcSwitch
    {
        public int Id { get; set; }
        public int IdPc { get; set; }
        public int IdSwitch { get; set; }

        public Pc IdPcNavigation { get; set; }
        public Switch IdSwitchNavigation { get; set; }
    }
}
