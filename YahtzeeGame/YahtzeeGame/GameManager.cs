using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeGame
{
    public class GameManager
    {
        public int Round;
       // public int Turn;
       // public int Initiative;
        public List<Player> players;
        public Dice[] Dicepool = new Dice[5];

        public GameManager()
        {
            Round = 0;

        }

    }
}
