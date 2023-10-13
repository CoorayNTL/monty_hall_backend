using Microsoft.AspNetCore.Mvc;
using MontyHallAPI.Models;


namespace MontyHallAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontyHallController : ControllerBase
    {



        private readonly DataContext _context;

        public MontyHallController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SimulationResult>>> GetMontyHalles()
        {
            return Ok(await _context.SimulationResults.ToListAsync());
        }

        [HttpPost("simulate")]
        public ActionResult<SimulationResult> SimulateMontyHall([FromBody] MontyHallSimulation input)
        {
            //handel input parameters
            int numberOfSimulations = input.NumberOfSimulations;
            bool switchDoor = input.SwitchDoor;

            int switchWins = 0;
            int stayWins = 0;

            //create reandom number for genereate process
            Random random = new Random();

            //handel the #simulation in this code bellow
            for (int i = 0; i < numberOfSimulations; i++)
            {
                List<int> doors = Enumerable.Range(1, 3).ToList(); //creat lit of door numbers
                int carPosition = random.Next(1, 4);//radom selec car 
                int initialChoice = random.Next(1, 4); //first choice 

                doors.Remove(initialChoice); //remove chooese doors initially 

                int hostReveal = doors.FirstOrDefault(d => d != carPosition); // Determine which door the host reveals

                //player going to choose to switch , player want to updated the first choice
                if (switchDoor)
                {
                    initialChoice = doors.FirstOrDefault(d => d != hostReveal);
                }

                // Check if the player's final choice matches the car's position
                if (initialChoice == carPosition)
                {
                    if (switchDoor)
                    {
                        switchWins++;
                    }
                    else
                    {
                        stayWins++;
                    }
                }
            }

            //create resulte using object for the win rate 
            SimulationResult result = new SimulationResult
            {
                TotalSimulations = numberOfSimulations,
                SwitchWins = switchWins,
                StayWins = stayWins,
                SwitchWinRate = Math.Min(1.0, Math.Max(0.0, (double)switchWins / numberOfSimulations)),
                StayWinRate = Math.Min(1.0, Math.Max(0.0, (double)stayWins / numberOfSimulations))
            };



            return result;
        }
    }

    public class DataContext
    {
    }
}
