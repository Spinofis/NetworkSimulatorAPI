using System;
using System.Collections.Generic;

namespace ComputerNetworkSimulatorAPI.Models.Database
{
    public partial class Simulation
    {
        public Simulation()
        {
            Links = new HashSet<Links>();
            Pc = new HashSet<Pc>();
            Router = new HashSet<Router>();
            Switch = new HashSet<Switch>();
        }

        public int Id { get; set; }
        public DateTime? DateAdd { get; set; }
        public DateTime? DateEdit { get; set; }
        public string Name { get; set; }

        public ICollection<Links> Links { get; set; }
        public ICollection<Pc> Pc { get; set; }
        public ICollection<Router> Router { get; set; }
        public ICollection<Switch> Switch { get; set; }
    }
}
