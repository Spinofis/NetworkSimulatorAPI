using ComputerNetworkSimulatorAPI.Interfaces;
using ComputerNetworkSimulatorAPI.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ComputerNetworkSimulatorAPI.Services
{
    public class TestFeature : ITestFeature
    {

        private readonly ComputerNetworkSimulatorContext _context;

        public TestFeature(ComputerNetworkSimulatorContext ctx)
        {
            _context = ctx;
        }
        public List<string> TestMethod()
        {
            return _context.TestTable.Select(x => x.Name).ToList();
        }
    }
}
