using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace monty_hall_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Monty_HallController : ControllerBase
    {
        [HttpGet("RunSimulation/monty-hall")]
        public ActionResult RunSimulation(int simulations, bool changeDoor)
        {
            var simulation = new Monty_hall(simulations, changeDoor);
            simulation.RunSimulations();
            return Ok(new
            {
                SwitchWinRate = simulation.GetSwitchWinRate(),
                StayWinRate = simulation.GetStayWinRate()
            });
        }
    }
}
