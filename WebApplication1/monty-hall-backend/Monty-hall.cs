namespace monty_hall_backend
{
    public class Monty_hall
    {
        private int totalSwitchWins = 0;
        private int totalStayWins = 0;
        private int simulations;
        private bool changeDoor; 
        private Random random = new Random(); // Single instance of Random to be used throughout the class 

        public Monty_hall(int simulations, bool changeDoor)
        {
            this.simulations = simulations;
            this.changeDoor = changeDoor;
        }

        public void RunSimulations()
        {
            for (int i = 0; i < this.simulations; i++) // Run the simulation the specified number of times
            {
                string[] doors = { "🐐", "🐐", "🐐" };
                int carIndex = random.Next(0, 3); // Use the single instance of Random
                doors[carIndex] = "🚗";

                int initialChoice = random.Next(0, 3); 
                int revealed;

                do
                {
                    revealed = random.Next(0, 3); 
                } while (revealed == initialChoice || doors[revealed] == "🚗"); // Keep revealing until a goat is revealed

                if (this.changeDoor) // If the player changes door, switch to the only other door that is not the initial choice or the revealed door
                {
                    initialChoice = 3 - initialChoice - revealed; // 0 + 1 + 2 = 3 (the sum of all the doors' indexes) so 3 - initialChoice - revealed will always be the remaining door
                }

                if (doors[initialChoice] == "🚗") 
                {
                    if (this.changeDoor)
                    {
                        this.totalSwitchWins++;
                    }
                    else
                    {
                        this.totalStayWins++;
                    }
                    /*this.totalSwitchWins++;*/
                }
              /*  else
                {
                    this.totalStayWins++;
                }*/
            }
        }

        public double GetSwitchWinRate()
        {
            if (this.simulations == 0) return 0; // check zero simulation  
            return (double)this.totalSwitchWins / this.simulations * 100;
        }

        public double GetStayWinRate()
        {
            if (this.simulations == 0) return 0; 
            return (double)this.totalStayWins / this.simulations * 100;
        }

    }
}