using ComputerNetworkSimulatorAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerNetworkSimulatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestFeatureController : ControllerBase
    {
        private readonly ITestFeature businessLogic;

        public TestFeatureController(ITestFeature businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        [HttpGet("TestMethod")]
        public ActionResult TestMethod()
        {
            return Ok(businessLogic.TestMethod());
        }

        [HttpPost("TestPost")]
        public ActionResult TestPost([FromBody] List<FatherDTO> obj)
        {
            return Ok(obj);
        }
    }

    public class Kid1DTO 
    {
        public string Kid1 { get; set; }
    }

    public class Kid2DTO 
    {
        public string Kid2 { get; set; }
    }

    public class FatherDTO
    {
        public Kid1DTO Kid1 { get; set; }
        public Kid2DTO Kid2 { get; set; }
    }
}
