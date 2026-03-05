using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace YahtzeeGame
{
    public class DumbBot : Player
    {
        public DumbBot (int Pos, string Name, bool Comp) : base(Pos, Name, Comp)
        {

        }

        public void DecisionTree(int[] dice)
        {
            if (!this.PlayerScores.isScoreCardFinished)
            {
                if (this.PlayerScores.YahtzeeValidation(dice) && !this.PlayerScores.YahtzeeScored)
                {
                    this.PlayerScores.YahtzeeSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Yahtzee, scoring " +
                                    PlayerScores.Yahtzee + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (this.PlayerScores.LargeStraightValidation(dice) && !this.PlayerScores.LargeStraightScored)
                {
                    this.PlayerScores.LargeStraightSelected( true);
                    MessageBox.Show("The Bot has made a decision. It has selected Large Straight, scoring " +
                                    PlayerScores.LargeStraight + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (this.PlayerScores.SmallStraightValidation(dice) && !this.PlayerScores.SmallStraightScored)
                {
                    this.PlayerScores.SmallStraightSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Small Straight, scoring " +
                                    PlayerScores.SmallStraight + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (this.PlayerScores.FourKindValidation(dice) && !this.PlayerScores.FourOfAKindScored)
                {
                    this.PlayerScores.FourOfAKindSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Four of a Kind, scoring " +
                                    PlayerScores.FourOfAKind + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (this.PlayerScores.ThreeKindValidation(dice) && !this.PlayerScores.ThreeOfAKindScored)
                {
                    this.PlayerScores.ThreeOfAKindSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Three of a Kind, scoring " +
                                    PlayerScores.ThreeOfAKind + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (this.PlayerScores.FullHouseValidation(dice) && !this.PlayerScores.FullHouseScored)
                {
                    this.PlayerScores.FullHouseSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Full House, scoring " +
                                    PlayerScores.FullHouse + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.SixesScored && this.PlayerScores.Sixes > 0)
                {
                    this.PlayerScores.SixesSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Sixes, scoring " +
                                    PlayerScores.Sixes + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.FivesScored && this.PlayerScores.Fives > 0)
                {
                    this.PlayerScores.FivesSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Fives, scoring " +
                                    PlayerScores.Fives + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.FoursScored && this.PlayerScores.Fours > 0)
                {
                    this.PlayerScores.FoursSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Fours, scoring " +
                                    PlayerScores.Fours + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.ThreesScored && this.PlayerScores.Threes > 0)
                {
                    this.PlayerScores.ThreesSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Threes, scoring " +
                                    PlayerScores.Threes + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.TwosScored && this.PlayerScores.Twos > 0)
                {
                    this.PlayerScores.TwosSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Twos, scoring " + PlayerScores.Twos +
                                    " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.AcesScored && this.PlayerScores.Aces > 0)
                {
                    this.PlayerScores.AcesSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Aces, scoring " + PlayerScores.Aces +
                                    " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.chanceScored)
                {
                    this.PlayerScores.ChanceSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Chance, scoring " +
                                    PlayerScores.Chance + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.AcesScored)
                {
                    this.PlayerScores.AcesSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Aces, scoring " + PlayerScores.Aces +
                                    " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.TwosScored)
                {
                    this.PlayerScores.TwosSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Twos, scoring " + PlayerScores.Twos +
                                    " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.ThreesScored)
                {
                    this.PlayerScores.ThreesSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Threes, scoring " +
                                    PlayerScores.Threes + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.FoursScored)
                {
                    this.PlayerScores.FoursSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Fours, scoring " +
                                    PlayerScores.Fours + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.FivesScored)
                {
                    this.PlayerScores.FivesSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Fives, scoring " +
                                    PlayerScores.Fives + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.SixesScored)
                {
                    this.PlayerScores.SixesSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Sixes, scoring " +
                                    PlayerScores.Sixes + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.ThreeOfAKindScored)
                {
                    this.PlayerScores.ThreeOfAKindSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Three of a Kind, scoring " +
                                    PlayerScores.ThreeOfAKind + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.FourOfAKindScored)
                {
                    this.PlayerScores.FourOfAKindSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Four of a Kind, scoring " +
                                    PlayerScores.FourOfAKind + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.FullHouseScored)
                {
                    this.PlayerScores.FullHouseSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Full House, scoring " +
                                    PlayerScores.FullHouse + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.SmallStraightScored)
                {
                    this.PlayerScores.SmallStraightSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Small Straight, scoring " +
                                    PlayerScores.SmallStraight + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.LargeStraightScored)
                {
                    this.PlayerScores.LargeStraightSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Large Straight, scoring " +
                                    PlayerScores.LargeStraight + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else if (!this.PlayerScores.YahtzeeScored)
                {
                    this.PlayerScores.YahtzeeSelected(true);
                    MessageBox.Show("The Bot has made a decision. It has selected Yahtzee, scoring " +
                                    PlayerScores.Yahtzee + " points, and " +
                                    "now has " + PlayerScores.TotalScore + " points.");
                }
                else
                {
                    MessageBox.Show("This computer is done making decisions");
                    this.PlayerScores.isScoreCardFinished = true;
                }
            }
        }
    }
}
