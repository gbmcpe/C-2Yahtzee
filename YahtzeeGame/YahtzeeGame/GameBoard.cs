using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace YahtzeeGame
{

    /// <summary>
    /// class for score rows
    /// </summary>
    public class ScoreRow
    {
        public string Score { get; set; }   
        public int? Player { get; set; }
        

        // Constructor
        public ScoreRow(string score)
        {
            Score = score;
        }
    }

    /// <summary>
    /// public class for scoreboard
    /// </summary>
    public class ScoreBoard
    {
        /// <summary>
        /// Create list for scoreboard
        /// </summary>
        public List<ScoreRow> Rows { get; set; }

        /// <summary>
        /// Call method to scoreboard class
        /// </summary>
        public ScoreBoard()
        {
            Rows = CreateDefaultRows();
        }

        /// <summary>
        /// Fields for list
        /// </summary>
        /// <returns></returns>
        private List<ScoreRow> CreateDefaultRows()
        {
            return new List<ScoreRow>
            {
                new ScoreRow("Ones"),
                new ScoreRow("Twos"),
                new ScoreRow("Threes"),
                new ScoreRow("Fours"),
                new ScoreRow("Fives"),
                new ScoreRow("Sixes"),

                new ScoreRow("Sum"),
                new ScoreRow("Bonus"),

                new ScoreRow("Three of a Kind"),
                new ScoreRow("Four of a Kind"),
                new ScoreRow("Full House"),
                new ScoreRow("Small Straight"),
                new ScoreRow("Large Straight"),
                new ScoreRow("Chance"),
                new ScoreRow("YAHTZEE"),

                new ScoreRow("Total Score")
            };
        }
    }
}





