using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_1._1._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateTimeController : ControllerBase
    {
        public DateTime CurrentTime = DateTime.Now;

        [HttpGet("/date")]
        public IActionResult GetDate([FromQuery] string format = "dd.MM.yyyy")
        {
            return Ok(CurrentTime.ToString(format));
        }

        [HttpGet("/time")]
        public IActionResult GetTime([FromQuery] string format = "HH:mm:ss")
        {
            return Ok(CurrentTime.ToString(format));
        }
    }
}
