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
        public ActionResult TestPost([FromBody] TestDTO obj)
        {
            return Ok(obj);
        }
    }

    public class TestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
