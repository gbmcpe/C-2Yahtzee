using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MultiplayerWindow.xaml
    /// </summary>
    public partial class MultiplayerWindow : Window
    {
        public MultiplayerWindow()
        {
            InitializeComponent();
            btnRollForPosition.IsEnabled = false;
            cmbxPlayers.Focus();
        }

        List<Player> Players = new List<Player>();
        Dice[] PlayerRolls = new Dice[4];
        int firstPlayer;


        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            int[] turnOrder = new int[4];
            turnOrder = DetermineOrder();

            Player player;
            DumbBot bot;
            if (tbPlayer1 != null && cmbxPlayerType1.SelectedIndex == 0)
            {
                player = new Player(turnOrder[0], tbPlayer1.Text); 
                Players.Add(player);
            }
            else if (tbPlayer1 != null && cmbxPlayerType1.SelectedIndex == 1)
            {
                bot = new DumbBot(turnOrder[0], tbPlayer1.Text, true);
                Players.Add(bot);
            }
            else if (tbPlayer1 != null && cmbxPlayerType1.SelectedIndex == 2)
            {
                tbPlayer1.Text = tbPlayer1.Text + " (CPU)";

                player = new Player(turnOrder[0], tbPlayer1.Text);
                Players.Add(player);
            }

            /// Add hard bot
            else if (tbPlayer1 != null && cmbxPlayerType1.SelectedIndex == 3)
            {
                tbPlayer1.Text = "Hard AI (CPU)";

                player = new Player(turnOrder[0], tbPlayer1.Text);
                Players.Add(player);
            }

            if (tbPlayer2 != null && tbPlayer2.IsEnabled == true && cmbxPlayerType2.SelectedIndex == 0)
            {
                player = new Player(turnOrder[1], tbPlayer2.Text);
                Players.Add(player);
            }
            else if (tbPlayer2 != null && cmbxPlayerType2.SelectedIndex == 1)
            {
                bot = new DumbBot(turnOrder[1], tbPlayer2.Text, true);
                Players.Add(bot);
            }
            else if (tbPlayer2 != null && cmbxPlayerType2.SelectedIndex == 2)
            {
                tbPlayer2.Text = tbPlayer2.Text + " (CPU)";

                player = new Player(turnOrder[1], tbPlayer2.Text);
                Players.Add(player);
            }

            /// Add hard bot
            else if (tbPlayer2 != null && cmbxPlayerType2.SelectedIndex == 3)
            {
                tbPlayer2.Text = "Hard AI (CPU)";

                player = new Player(turnOrder[1], tbPlayer2.Text);
                Players.Add(player);
            }

            if (tbPlayer3 != null && tbPlayer3.IsEnabled == true && cmbxPlayerType3.SelectedIndex == 0)
            {
                player = new Player(turnOrder[2], tbPlayer3.Text);
                Players.Add(player);
            }
            else if (tbPlayer3 != null && cmbxPlayerType3.SelectedIndex == 1)
            {
                bot = new DumbBot(turnOrder[2], tbPlayer3.Text, true);
                Players.Add(bot);
            }
            else if (tbPlayer3 != null && cmbxPlayerType3.SelectedIndex == 2)
            {
                tbPlayer3.Text = tbPlayer3.Text + " (CPU)";

                player = new Player(turnOrder[2], tbPlayer3.Text);
                Players.Add(player);
            }

            /// Add hard bot
            else if (tbPlayer3 != null && cmbxPlayerType3.SelectedIndex == 3)
            {
                tbPlayer3.Text = "Hard AI (CPU)";

                player = new Player(turnOrder[2], tbPlayer3.Text);
                Players.Add(player);
            }

            if (tbPlayer4 != null && tbPlayer4.IsEnabled == true && cmbxPlayerType4.SelectedIndex == 0)
            {
                player = new Player(turnOrder[3], tbPlayer4.Text);
                Players.Add(player);
            }
            else if (tbPlayer4 != null && cmbxPlayerType4.SelectedIndex == 1)
            {
                bot = new DumbBot(turnOrder[3], tbPlayer4.Text, true);
                Players.Add(bot);
            }
            else if (tbPlayer4 != null && cmbxPlayerType4.SelectedIndex == 2)
            {
                tbPlayer4.Text = tbPlayer4.Text + " (CPU)";

                player = new Player(turnOrder[3], tbPlayer4.Text);
                Players.Add(player);
            }

            /// Add hard bot
            else if (tbPlayer4 != null && cmbxPlayerType4.SelectedIndex == 3)
            {
                tbPlayer4.Text = "Hard AI (CPU)";

                player = new Player(turnOrder[3], tbPlayer4.Text);
                Players.Add(player);
            }

            Players = Players.OrderBy(p => p.PlayerPos).ToList();

            //PUT PLAYERS LIST IN HERE AND THEN CATCH IT IN ITS CONSTRUCTOR!
            GameWindow gameWindow = new GameWindow(Players);
            gameWindow.Show();


            this.Close();
        }

        private int[] DetermineOrder()
        {
            int[] order = new int[4];

            if (firstPlayer == 0)
            {
                order[0] = 0;
                order[1] = 1;
                order[2] = 2;
                order[3] = 3;
            }

            if (firstPlayer == 1)
            {
                order[0] = 3;
                order[1] = 0;
                order[2] = 1;
                order[3] = 2;
            }

            if (firstPlayer == 2)
            {
                order[0] = 2;
                order[1] = 3;
                order[2] = 0;
                order[3] = 1;
            }

            if (firstPlayer == 3)
            {
                order[0] = 1; 
                order[1] = 2;
                order[2] = 3;
                order[3] = 0;
            }
            
            return order;
        }

        private void cmbxPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbxPlayers.SelectedIndex == 0)
            {
                tbPlayer1.IsEnabled = false;
                tbPlayer2.IsEnabled = false;
                tbPlayer3.IsEnabled = false;
                tbPlayer4.IsEnabled = false;
                lblPlayerName.Visibility = Visibility.Hidden;


            }
            else if (cmbxPlayers.SelectedIndex == 1)
            {
                tbPlayer1.IsEnabled = true;
                tbPlayer2.IsEnabled = false;
                tbPlayer3.IsEnabled = false;
                tbPlayer4.IsEnabled = false;
                lblPlayerName.Visibility = Visibility.Visible;
            }
            else if (cmbxPlayers.SelectedIndex == 2)
            {
                tbPlayer1.IsEnabled = true;
                tbPlayer2.IsEnabled = true;
                tbPlayer3.IsEnabled = false;
                tbPlayer4.IsEnabled = false;
                lblPlayerName.Visibility = Visibility.Visible;
            }
            else if (cmbxPlayers.SelectedIndex == 3)
            {
                tbPlayer1.IsEnabled = true;
                tbPlayer2.IsEnabled = true;
                tbPlayer3.IsEnabled = true;
                tbPlayer4.IsEnabled = false;
                lblPlayerName.Visibility = Visibility.Visible;
            }
            else if (cmbxPlayers.SelectedIndex == 4)
            {
                tbPlayer1.IsEnabled = true;
                tbPlayer2.IsEnabled = true;
                tbPlayer3.IsEnabled = true;
                tbPlayer4.IsEnabled = true;
                lblPlayerName.Visibility = Visibility.Visible;
            }
        }

        private void tbPlayer1_TextChanged(object sender, TextChangedEventArgs e)
            {
            if (tbPlayer1 != null && tbPlayer2.IsEnabled == false)
            {
               btnStart.IsEnabled=CheckStart();
            }
            else if (tbPlayer1 != null && tbPlayer2.IsEnabled==true)
            { btnRollForPosition.IsEnabled = CheckStart(); }
         

        }

        private bool CheckStart()
        {
            if ((tbPlayer1 != null) && (tbPlayer2 != null || tbPlayer2.IsEnabled ==false )
                                    && (tbPlayer3 != null || tbPlayer3.IsEnabled == false)
                                    && (tbPlayer3 != null || tbPlayer3.IsEnabled == false))
            {
              
                return true;
            }
            
            return false;
        }

        private void tbPlayer2_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnRollForPosition.IsEnabled = CheckStart();

        }

        private void tbPlayer3_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnRollForPosition.IsEnabled = CheckStart();

        }

        private void tbPlayer4_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnRollForPosition.IsEnabled = CheckStart();

        }

        private void btnRollPos_Click(object sender, RoutedEventArgs e)
        {
            lblPlayer1_Roll.Content = "";
            lblPlayer2_Roll.Content = "";
            lblPlayer3_Roll.Content = "";
            lblPlayer4_Roll.Content = "";

            PlayerRolls[0] = new Dice();
            PlayerRolls[1] = new Dice();
            PlayerRolls[2] = new Dice();
            PlayerRolls[3] = new Dice();
            Random random = new Random();

            for (int c = 0; c < 4; c++) { PlayerRolls[c].RollDice(random); }

            if (tbPlayer1 != null)
            {
                lblPlayer1_Roll.Content = $"({PlayerRolls[0].diceSum})";
                if (tbPlayer2 != null) { lblPlayer2_Roll.Content = $"({PlayerRolls[1].diceSum})"; }
                if (tbPlayer3 != null && tbPlayer3.IsEnabled == true) { lblPlayer3_Roll.Content = $"({PlayerRolls[2].diceSum})"; }
                if (tbPlayer4 != null && tbPlayer4.IsEnabled == true) { lblPlayer4_Roll.Content = $"({PlayerRolls[3].diceSum})"; }
            }

            int[] rolls = new int[4];
            rolls[0] = PlayerRolls[0].diceSum;
            rolls[1] = PlayerRolls[1].diceSum;
            rolls[2] = PlayerRolls[2].diceSum;
            rolls[3] = PlayerRolls[3].diceSum;

            int max = rolls.Max();
            int index = 0;

            foreach (int roll in rolls)
            {
                if (max == roll)
                {
                    firstPlayer = index;
                }

                index++;
            }

            if (CheckStart() && NoTies(rolls))
            {
                btnStart.IsEnabled = true;
            }
            else { btnStart.IsEnabled = false; }
        }

        private bool NoTies(int[] rolls)
        {
            int max = rolls.Max();
            int counter = 0;

            foreach (int roll in rolls)
            {
                if (max == roll)
                {
                    counter++;
                }
            }

            if (counter >= 2)
            {
                return false;
            }
            return true;
        }
        #region MediumBot
        /// <summary>
        /// Converts the name "Medium Bot" into a CPU player.
        /// </summary>
        /// <param name="rawName">The original name entered by the user.</param>
        /// <returns>Modified name if CPU, otherwise original name.</returns>
        private string CpuNameIfEasyBot(string rawName)
        {
            /// If the textbox value is null, return it unchanged.
            if (rawName == null) return rawName;

            /// Compare trimmed name to "Easy Bot" ignoring case.
            if (rawName.Trim().Equals("Medium Bot", StringComparison.OrdinalIgnoreCase))
            {
                /// Return the modified CPU-tagged name.
                return "Medium Bot (CPU)";
            }

            /// If not Easy Bot, return the original name.
            return rawName;
        }



        #endregion

        private void cmbxPlayerType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void cmbxPlayerType2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



        }

        private void cmbxPlayerType3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbxPlayerType4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}