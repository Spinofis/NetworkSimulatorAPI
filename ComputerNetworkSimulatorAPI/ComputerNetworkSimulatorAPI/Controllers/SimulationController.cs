using ComputerNetworkSimulatorAPI.Interfaces;
using ComputerNetworkSimulatorAPI.Models.SimulationDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : ControllerBase
    {
        private readonly ISimulationService businessLogic;

        public SimulationController(ISimulationService businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        [HttpGet("TestMethod")]
        public ActionResult TestMethod()
        {
            return Ok("dupa123");
        }

        [HttpPost("SaveSimulation")]
        public ActionResult SaveSimulation(SimulationDTO simulation)
        {
            businessLogic.SaveSimulation(simulation);
            return Ok(simulation);
        }
    }
}
