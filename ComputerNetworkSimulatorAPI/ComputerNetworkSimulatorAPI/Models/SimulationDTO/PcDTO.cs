﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Models.SimulationDTO
{
    public class PcDTO
    {
        public int Id { get; set; }
        public string NodeNumber { get; set; }
        public int? PcNumber { get; set; }
        public string Name { get; set; }
        public string HostIdentity { get; set; }

    }
}
