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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YahtzeeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private bool[] CheckDice()
        {
            bool[] dice = new bool[4];
            dice[0] = cbDie1.IsChecked ?? false;
            dice[1] = cbDie2.IsChecked ?? false;
            dice[2] = cbDie3.IsChecked ?? false;
            dice[3] = cbDie4.IsChecked ?? false;
            dice[4] = cbDie5.IsChecked ?? false;
            return dice;
        }

        /// <summary>
        /// Closes Program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnRollDice_Click(object sender, RoutedEventArgs e)
        {
            bool[] DiceState = CheckDice();

            for(int c = 0; c <5; c++)
            {
                if (!DiceState[c])
                {
                    if (int.Parse(lblTimesRolled.ContentStringFormat) >3)
                    {

                        Random random = new Random();
                        
                        int DieValue = random.Next(1, 7);

                        if(c == 0) 
                        {
                            cbDie1.ContentStringFormat = DieValue.ToString();
                            Die1.Source = new BitmapImage(new Uri($@"Die{DieValue}.bmp", UriKind.Relative));
                        }
                        else if (c == 1)
                        {
                            cbDie2.ContentStringFormat = DieValue.ToString();
                            Die2.Source = new BitmapImage(new Uri($@"Die{DieValue}.bmp", UriKind.Relative));
                        }
                        else if (c == 2)
                        {
                            cbDie3.ContentStringFormat = DieValue.ToString();
                            Die3.Source = new BitmapImage(new Uri($@"Die{DieValue}.bmp", UriKind.Relative));
                        }
                        else if (c == 3)
                        {
                            cbDie4.ContentStringFormat = DieValue.ToString();
                            Die4.Source = new BitmapImage(new Uri($@"Die{DieValue}.bmp", UriKind.Relative));
                        }
                        else if (c == 4)
                        {
                            cbDie5.ContentStringFormat = DieValue.ToString();
                            Die5.Source = new BitmapImage(new Uri($@"Die{DieValue}.bmp", UriKind.Relative));
                        }
                    }

                }
            }
        }
    }
}
