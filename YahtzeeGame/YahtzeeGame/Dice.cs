using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeGame
{
    public class Dice
    {
        public int Die1Value { get; set; }
        public int Die2Value { get; set; }
        public int Die3Value { get; set; }
        public int Die4Value { get; set; }
        public int Die5Value { get; set; }

        public int NumberOfTimesRolled { get; set; }

        public bool HoldDie1 { get; set; }
        public bool HoldDie2 { get; set; }
        public bool HoldDie3 { get; set; }
        public bool HoldDie4 { get; set; }
        public bool HoldDie5 { get; set; }

        private List<int> diceValues;
        private List<bool> holdValues;


        public Dice()
        {
            NumberOfTimesRolled = 0;
            Die1Value = 0;
            Die2Value = 0;
            Die3Value = 0;
            Die4Value = 0;
            Die5Value = 0;
            HoldDie1 = false;
            HoldDie2 = false;
            HoldDie3 = false;
            HoldDie4 = false;
            HoldDie5 = false;
        }

        public void Roll()
        {
            if (NumberOfTimesRolled < 3)
            {
                Random random = new Random();
                if (!HoldDie1)
                {
                    Die1Value = random.Next(1, 7);
                    Die2Value = random.Next(1, 7);
                    Die3Value = random.Next(1, 7);
                    Die4Value = random.Next(1, 7);
                    Die5Value = random.Next(1, 7);
                }
            }

        }

    }

}
