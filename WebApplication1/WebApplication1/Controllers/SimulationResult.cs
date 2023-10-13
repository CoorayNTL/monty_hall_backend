namespace MontyHallAPI.Models
{
    public class SimulationResult // use get the resulte of simulation 
    {
        public int TotalSimulations { get; set; }
        public int SwitchWins { get; set; }
        public int StayWins { get; set; }
        public double SwitchWinRate { get; set; }
        public double StayWinRate { get; set; }
    }
}
