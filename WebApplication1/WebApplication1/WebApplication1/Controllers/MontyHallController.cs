using Microsoft.AspNetCore.Mvc;
using MontyHallAPI.Models;
using System;
using System.Collections.Generic;

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

            for (int i = 0; i < numberOfSimulations; i++)
            {
                List<string> doors = new List<string> { "Goat", "Goat", "Goat" };
                int carIndex = random.Next(3);
                doors[carIndex] = "Car";

                int initialChoice = random.Next(3);
                int revealed = -1;

                do
                {
                    revealed = random.Next(3);
                } while (revealed == initialChoice || doors[revealed] == "Car");

                if (switchDoor)
                {
                    initialChoice = 3 - initialChoice - revealed;
                }

                if (doors[initialChoice] == "Car")
                {
                    switchWins++;
                }
                else
                {
                    stayWins++;
                }
            }

            double switchWinRate = (double)switchWins / numberOfSimulations;
            double stayWinRate = (double)stayWins / numberOfSimulations;

            SimulationResult result = new SimulationResult
            {
                TotalSimulations = numberOfSimulations,
                SwitchWins = switchWins,
                StayWins = stayWins,
                SwitchWinRate = switchWinRate,
                StayWinRate = stayWinRate
            };

            return result;
        }
    }

    public class DataContext
    {
    }
}
