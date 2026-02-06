using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeGame
{
    internal class Player
    {


        private int PlayerPos;
        private string PlayerName;
        private bool ComputerPlayer;
        // private ScoreColumn PlayerScores;
        public int SelectedScore;

        //yadda yadda
        //constructors
        public Player(int Pos) 
        { 
            PlayerPos = Pos;
            PlayerName = ($"Player {Pos}");
            ComputerPlayer = false;
        }

        public Player(int Pos, string Name)
        {
            PlayerPos = Pos;
            PlayerName = Name;
            ComputerPlayer = false;
        }

        public Player(int Pos, string Name, bool Comp)
        {
            PlayerPos = Pos;
            PlayerName = Name;
            ComputerPlayer = Comp;

        }


        private void DisplayScore()
        {
            
        }


        private void UpdateScoreColumn()
        {
            
        }



    }
}
