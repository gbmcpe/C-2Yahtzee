using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeGame
{
    public class Dice
    {




        public int[] diceValue;
        private Random random;


        public Dice()
        {
            diceValue = new int[5];
            random = new Random();

        }

        public void RollDice(bool[] dicestates)
        {
            for (int c = 0; c < 5; c++)
            {
                //If Statement Triggers if a die is unchecked. Checked Dice do not reroll.

                if (dicestates[c] == false)
                {
                   diceValue[c] = random.Next(1, 7);

                    //Displays rolled die face. c = die face.

                }
            }



        }

    }

}
