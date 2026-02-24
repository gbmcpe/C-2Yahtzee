using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
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
        //List of players. Will be used to track player information and scores.
        List<Player> Players = new List<Player>();

        readonly ScoreBoard scoreBoard;

        GameManager YahtzeeGame = new GameManager();


        public MainWindow()
        {
          InitializeComponent();
        }

        

        private void singlePlayerbtt_Click(object sender, RoutedEventArgs e)
        {
            //Create a instance of a player
            //add it to the player list
            //and hide the start screen.

            Player singlePlayer = new Player(1);
            Players.Add(singlePlayer);
        }
        private void getNameBtt_Click(object sender, RoutedEventArgs e)
        {
            //Takes the text from the name input box,
            //and sets it as the player's name.
            //If the input is empty, prompts the user to enter a name.
            string name = nameTxtBx.Text;
            if (name != "")
            {
                Players[0].PlayerName = name;
               }
            else
            {
                MessageBox.Show("Please enter a name.");
            }

        }

        private void btnAbout(object sender, RoutedEventArgs e)
        {
            //Program Information
            MessageBox.Show("Yahtzee Version 0.1. Made By Marcus Cantrall, Bradye Vanderheyden,Connor Orton, Nicole Gonzalez Rodriguez and Beau Baker. ");
        }

        private void btnHighScore_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<int, string> Scores = new Dictionary<int, string>();

            List<int> ScoreNum = new List<int>();

            String[] LineSplit = new string[2];

            StreamReader Input = File.OpenText("HighScores.txt");

            while (!Input.EndOfStream)
            {
                LineSplit = Input.ReadLine().Split(' ');


                Scores.Add(int.Parse(LineSplit[0]), LineSplit[1]);

                ScoreNum.Add(int.Parse(LineSplit[0]));

            }

            ScoreNum.OrderByDescending(score => score);

            int[] topFive = new int[5];

            for (int i = 0; i < 5; i++)
            {
                topFive[i] = ScoreNum[i];
            }

            MessageBox.Show($"1. {topFive[0]}, {Scores[topFive[0]]} \n2.  {topFive[1]}, {Scores[topFive[1]]} \n3. {topFive[2]}, {Scores[topFive[2]]}\n4. {topFive[3]}, {Scores[topFive[3]]}\n5. {topFive[4]}, {Scores[topFive[4]]}  ");

        }
    }
    
}
       

