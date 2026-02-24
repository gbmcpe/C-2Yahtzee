using System;
using System.Collections.Generic;
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
        }
        List<Player> Players = new List<Player>();
        Dice[] PlayerRolls = new Dice[4];

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Player player;
            if (tbPlayer1 != null)
            {
                player = new Player(1, tbPlayer1.Text); 
                Players.Add(player);
            }

            if (tbPlayer2 != null)
            {
                player = new Player(2, tbPlayer2.Text);
                Players.Add(player);
            }

            if (tbPlayer3 != null && tbPlayer3.IsEnabled == true)
            {
                player = new Player(3, tbPlayer3.Text);
                Players.Add(player);
            }

            if (tbPlayer4 != null && tbPlayer4.IsEnabled == true)
            {
                player = new Player(4, tbPlayer4.Text);
                Players.Add(player);
            }

            //PUT PLAYERS LIST IN HERE AND THEN CATCH IT IN ITS CONSTRUCTOR!
            GameWindow gameWindow = new GameWindow(Players);
            gameWindow.Show();
            this.Close();
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
                tbPlayer2.IsEnabled = true;
                tbPlayer3.IsEnabled = false;
                tbPlayer4.IsEnabled = false;
                lblPlayerName.Visibility = Visibility.Visible;
            }
            else if (cmbxPlayers.SelectedIndex == 2)
            {
                tbPlayer1.IsEnabled = true;
                tbPlayer2.IsEnabled = true;
                tbPlayer3.IsEnabled = true;
                tbPlayer4.IsEnabled = false;
                lblPlayerName.Visibility = Visibility.Visible;
            }
            else if (cmbxPlayers.SelectedIndex == 3)
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
            btnRollForPosition.IsEnabled = CheckStart();

        }

        private bool CheckStart()
        {
            if ((tbPlayer1 != null) && (tbPlayer2 != null)
                                    && (tbPlayer3 != null || tbPlayer3.IsEnabled == false)
                                    && (tbPlayer3 != null || tbPlayer3.IsEnabled == false))
            {
                btnRollForPosition.IsEnabled = true;
                return true;
            }
            else return false;
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
            if (CheckStart())
            {
                btnStart.IsEnabled = true;
            }
            else { btnStart.IsEnabled = false; btnRollForPosition.IsEnabled = false; }


        }
    }
}