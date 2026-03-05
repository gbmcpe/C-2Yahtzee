using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YahtzeeGame
{
    /// <summary>
    /// Interaction logic for GameReview.xaml
    /// </summary>
    public partial class GameReview : Window
    {
        public GameReview(List<Player> Players, bool gameOver)
        {
            InitializeComponent();

            StoredPlayers = Players;
            GameOver = gameOver;

            ApplyEndState(Players);

        }

       private List<Player> StoredPlayers;

       private bool GameOver { get; }

        private void FillBoxes(Player currentPlayer, int n)
        {


            ((Label)this.FindName($"lblPlayer{n}")).Content = currentPlayer.PlayerName;

            if (currentPlayer.PlayerScores.acesScored)
            {
                ((TextBox)this.FindName($"tbAcesP{n}")).Text = currentPlayer.PlayerScores.aces.ToString();
            }
            else
            { ((TextBox)this.FindName($"tbAcesP{n}")).Text = ""; }

            if (currentPlayer.PlayerScores.twosScored)
            {
                ((TextBox)this.FindName($"tbTwosP{n}")).Text = currentPlayer.PlayerScores.twos.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"tbTwosP{n}")).Text = "";
            }

            if (currentPlayer.PlayerScores.threesScored)
            {
                ((TextBox)this.FindName($"tbThreesP{n}")).Text = currentPlayer.PlayerScores.threes.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"tbThreesP{n}")).Text = "";
            }


            if (currentPlayer.PlayerScores.foursScored)
            {
                ((TextBox)this.FindName($"tbFoursP{n}")).Text = currentPlayer.PlayerScores.fours.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"tbFoursP{n}")).Text = "";
            }

            if (currentPlayer.PlayerScores.fivesScored)
            {
                ((TextBox)this.FindName($"tbFivesP{n}")).Text = currentPlayer.PlayerScores.fives.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"tbFivesP{n}")).Text = "";
            }

            if (currentPlayer.PlayerScores.sixesScored)
            {
                ((TextBox)this.FindName($"tbSixesP{n}")).Text = currentPlayer.PlayerScores.sixes.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"tbSixesP{n}")).Text = "";
            }

            /*
            if (currentPlayer.PlayerScores.bonusScored)
            {
                tbBonus.Text = currentPlayer.PlayerScores.bonus.ToString();
            }
            */
            if (currentPlayer.PlayerScores.threeOfAKindScored)
            {
                ((TextBox)this.FindName($"tbThreeKindP{n}")).Text = currentPlayer.PlayerScores.threeOfAKind.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"tbThreeKindP{n}")).Text = "";
            }

            if (currentPlayer.PlayerScores.fourOfAKindScored)
            {
                ((TextBox)this.FindName($"tbFourKindP{n}")).Text = currentPlayer.PlayerScores.fourOfAKind.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"tbFourKindP{n}")).Text = "";
            }

            if (currentPlayer.PlayerScores.fullHouseScored)
            {
                ((TextBox)this.FindName($"tbFullHouseP{n}")).Text = currentPlayer.PlayerScores.fullHouse.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"FulLHouseP{n}")).Text = "";
            }

            if (currentPlayer.PlayerScores.smallStraightScored)
            {
                ((TextBox)this.FindName($"tbSmallStraightP{n}")).Text = currentPlayer.PlayerScores.smallStraight.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"tbSmallStraightP{n}")).Text = "";
            }

            if (currentPlayer.PlayerScores.largeStraightScored)
            {
                ((TextBox)this.FindName($"tbLargeStraightP{n}")).Text = currentPlayer.PlayerScores.largeStraight.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"tbLargeStraightP{n}")).Text = "";
            }

            if (currentPlayer.PlayerScores.yahtzeeScored)
            {
                ((TextBox)this.FindName($"tbYahtzeeP{n}")).Text = currentPlayer.PlayerScores.yahtzee.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"tbYahtzeeP{n}")).Text = "";
            }

            if (currentPlayer.PlayerScores.chanceScored)
            {
                ((TextBox)this.FindName($"tbChanceP{n}")).Text = currentPlayer.PlayerScores.chance.ToString();
            }
            else
            {
                ((TextBox)this.FindName($"tbChanceP{n}")).Text = "";
            }


    
           
                ((TextBox)this.FindName($"tbTotalP{n}")).Text = currentPlayer.PlayerScores.totalScore.ToString();
            
          
            
        }

        private void ApplyEndState(List<Player> players)
        {
            int n = 1;
            Player winner = null;
            Player tier = null;
            bool tie = false;

            foreach (Player p in players)
            {
               

                FillBoxes(p, n);
                if (winner == null) { winner = p; }
                else if (p.PlayerScores.totalScore > winner.PlayerScores.totalScore)
                {
                    winner = p;
                    tie = false;
                    tier = null;
                }
                else if (p.PlayerScores.totalScore == winner.PlayerScores.totalScore)
                {
                    tier = p;
                    tie = true;
                }
                n++;
            }

            if (GameOver == true)
            {
                if (tie) { lblWinner.Content = "Tie!"; lblWinnerName.Content = ""; }
                else { lblWinner.Content = "Wins!"; lblWinnerName.Content = winner.PlayerName; }
            }
            else { lblWinner.Content = ""; lblWinnerName.Content = ""; btnReturnToGame.Content = "Return to Game"; }

        }

        private void RecordHighScores(Player P)
        {
            StreamWriter Output = new StreamWriter("HighScores.txt", true);
            Output.WriteLine($"{P.PlayerScores.totalScore} {P.PlayerName}");
            Output.Close();
        }


        private void btnReturnToGame_Click(object sender, RoutedEventArgs e)
        {
            if (GameOver)
            {
                foreach (Player p in StoredPlayers)
                { p.PlayerScores = new ScoreCard(); }
                GameWindow gamewindow = new GameWindow(StoredPlayers);
                gamewindow.Show();
                this.Close();
            }
            else
            {
                GameWindow gamewindow = new GameWindow(StoredPlayers);
                gamewindow.Show();
                this.Close();
            }

        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }



        private void btnSaveScoreP1_Click(object sender, RoutedEventArgs e)
        {
            RecordHighScores(StoredPlayers[0]);
            btnSaveScoreP1.Content = "Score Saved.";
                btnSaveScoreP1.IsEnabled = false;
        }

        private void btnSaveScoreP2_Click(object sender, RoutedEventArgs e)
        {
            RecordHighScores(StoredPlayers[1]);
            btnSaveScoreP2.Content = "Score Saved.";
            btnSaveScoreP2.IsEnabled = false;
        }

        private void btnSaveScoreP3_Click(object sender, RoutedEventArgs e)
        {
            RecordHighScores(StoredPlayers[2]);
            btnSaveScoreP3.Content = "Score Saved.";
            btnSaveScoreP3.IsEnabled = false;
        }

        private void btnSaveScoreP4_Click(object sender, RoutedEventArgs e)
        {
            RecordHighScores(StoredPlayers[3]);
            btnSaveScoreP4.Content = "Score Saved.";
            btnSaveScoreP4.IsEnabled = false;
        }
    }
}
