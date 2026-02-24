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
        

        public int diceSum { get { return diceValue[0] + diceValue[1] + diceValue[2] + diceValue[3] + diceValue[4]; } }

        private Random random;

        public Dice()
        {
            diceValue = new int[5];

        }

        public void RollDice(bool[] dicestates, Random r)
        {
     

            for (int c = 0; c < 5; c++)
            {
                //If Statement Triggers if a die is unchecked. Checked Dice do not reroll.

                if (dicestates[c] == false)
                {
                    diceValue[c] = r.Next(1, 7);

                    //Displays rolled die face. c = die face.

                }
            }

        }
        public void RollDice(Random r)
        {
           
            for (int c = 0; c < 5; c++)
            {
     
                    diceValue[c] = r.Next(1, 7);
                
            }

        }
    }

}
