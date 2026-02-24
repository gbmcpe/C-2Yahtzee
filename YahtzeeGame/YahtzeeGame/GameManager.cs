using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeGame
{
    public class GameManager
    {


        public int Round;
        public int Turn;
        // public int Initiative;
        public List<Player> players;
        public int Rolls;
        public Dice Pool;
        public Player currentPlayer;
        private Random rand;

        public GameManager()
        {
            Round = 1;
            Turn = 1;
            Rolls = 3;
            rand = new Random((int)DateTime.Now.Ticks);

        }

        public void EndTurn()
        {
            //If this statement is true, the current round should end and return to the first player.
            if (this.Turn > players.Count())
            {
                Round++;
                Turn = 1;
                currentPlayer = players[Turn - 1];
            }

      

            //If this statement is true, the current round should continue and move to the next player in order.
            else if (this.Turn <= players.Count())
            {
                currentPlayer = players[Turn - 1];
            
            }


        }

        public void RollUsed(bool[]dicestate)
        {
           
            Rolls--;
            Pool.RollDice(dicestate, rand);

        }

    }
}
