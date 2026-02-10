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
            bool[] dice = new bool[5];
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





            int counter = int.Parse(lblTimesRolled.ContentStringFormat);

            lblTimesRolled.ContentStringFormat = (counter-1).ToString();
            lblTimesRolled.Content = (counter - 1).ToString();

            bool[] DiceState = CheckDice();

            Random random = new Random();

            for (int c = 0; c <5; c++)
            {
                if (DiceState[c]==false)
                {

                        

                    int DieValue = random.Next(1, 7);

                    if (c == 0)
                    {
                        cbDie1.ContentStringFormat = DieValue.ToString();

                        Die1.Source = new BitmapImage(new Uri($@"{DieValue}Die.bmp", UriKind.Relative));

                    }
                        else if (c == 1)
                        {
                            cbDie2.ContentStringFormat = DieValue.ToString();
                            Die2.Source = new BitmapImage(new Uri($@"{DieValue}Die.bmp", UriKind.Relative));
                        }
                        else if (c == 2)
                        {
                            cbDie3.ContentStringFormat = DieValue.ToString();
                            Die3.Source = new BitmapImage(new Uri($@"{DieValue}Die.bmp", UriKind.Relative));
                    }
                        else if (c == 3)
                        {
                            cbDie4.ContentStringFormat = DieValue.ToString();
                            Die4.Source = new BitmapImage(new Uri($@"{DieValue}Die.bmp", UriKind.Relative));
                    }
                        else if (c == 4)
                        {
                            cbDie5.ContentStringFormat = DieValue.ToString();
                            Die5.Source = new BitmapImage(new Uri($@"{DieValue}Die.bmp", UriKind.Relative));
                    }

                }
            }

            if (int.Parse(lblTimesRolled.ContentStringFormat) == 0)
            {

                TurnActivation(false);

            }


        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            TurnActivation(true);
            btnStart.IsEnabled = false;
        }

        private void TurnActivation(bool b)
        {
            cbDie1.IsEnabled = b;
            cbDie2.IsEnabled = b;
            cbDie3.IsEnabled = b;
            cbDie4.IsEnabled = b;
            cbDie5.IsEnabled = b;
            BtnRollDice.IsEnabled = b;

            cbDie1.IsChecked = false;
            cbDie2.IsChecked = false;
            cbDie3.IsChecked = false;
            cbDie4.IsChecked = false;
            cbDie5.IsChecked = false;


        }

        private void Reset()
        {


            Die1.Source = new BitmapImage(new Uri($@"1Die.bmp", UriKind.Relative));
            Die2.Source = new BitmapImage(new Uri($@"2Die.bmp", UriKind.Relative));
            Die3.Source = new BitmapImage(new Uri($@"3Die.bmp", UriKind.Relative));
            Die4.Source = new BitmapImage(new Uri($@"4Die.bmp", UriKind.Relative));
            Die5.Source = new BitmapImage(new Uri($@"5Die.bmp", UriKind.Relative));

            lblRollCount.Content = "3";
            lblRollCount.ContentStringFormat = "3";
            btnStart.IsEnabled = true;

        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            TurnActivation(false);

        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Yahtzee Version 0.1. Made By Marcus Cantrall, Bradye Vanderheyden,Connor Orton, Nicole Gonzalez Rodriguez and Beau Baker. ");
        }
    }
}
