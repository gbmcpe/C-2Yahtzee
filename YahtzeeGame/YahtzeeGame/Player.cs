using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeGame
{
    public class Player
    {

        //PlayerPos will start at 0.
        private int playerPos;
        private string playerName;
        private bool computerPlayer;
        private ScoreCard playerScores;
        private int selectedScore;

        //constructors
        public Player(int Pos) 
        { 
            PlayerPos = Pos;
            PlayerName = ($"Player {Pos}");
            ComputerPlayer = false;
            PlayerScores = new ScoreCard();
        }

        public Player(int Pos, string Name)
        {
            PlayerPos = Pos;
            PlayerName = Name;
            ComputerPlayer = false;
            PlayerScores = new ScoreCard();
        }

        public Player(int Pos, string Name, bool Comp)
        {
            PlayerPos = Pos;
            PlayerName = Name;
            ComputerPlayer = Comp; 
            PlayerScores = new ScoreCard();
        }

        //Set and get  the name property 
        public string PlayerName
        {
            get { return PlayerName1; }
            set { PlayerName1 = value; }
        }

        public int PlayerPos { get => playerPos; set => playerPos = value; }
        public string PlayerName1 { get => playerName; set => playerName = value; }
        public bool ComputerPlayer { get => computerPlayer; set => computerPlayer = value; }
        public ScoreCard PlayerScores { get => playerScores; set => playerScores = value; }
        public int SelectedScore { get => selectedScore; set => selectedScore = value; }



        //void set()
        //{
        //    List<int> ScoreList = new List<int>();
        //    ScoreList.Add(1);
        //    if (ScoreList.Contains(1)
        //        {

        //        int Score = ScoreList.
        //    }
        //    ;
        //}


        private void DisplayScore()
        {
            
        }


        private void UpdateScoreColumn()
        {
            
        }



    }
}
