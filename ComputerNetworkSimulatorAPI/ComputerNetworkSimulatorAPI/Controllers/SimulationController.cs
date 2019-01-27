using ComputerNetworkSimulatorAPI.Interfaces;
using ComputerNetworkSimulatorAPI.Models.SimulationDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerNetworkSimulatorAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : ControllerBase
    {
        private readonly ISimulationService businessLogic;

        public SimulationController(ISimulationService businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        [HttpPost("SaveSimulation")]
        public ActionResult SaveSimulation(SimulationDTO simulation)
        {
            try
            {
                var output = businessLogic.SaveSimulation(simulation);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetSimulationList")]
        public ActionResult GetSimulationList()
        {
            try
            {
                var output = businessLogic.GetSimulationList();
                if (output == null) return NotFound();
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetSimulation")]
        public ActionResult GetSimulation(int simId)
        {
            try
            {
                var output = businessLogic.GetSimulation(simId);
                if (output == null) return NotFound();
                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
