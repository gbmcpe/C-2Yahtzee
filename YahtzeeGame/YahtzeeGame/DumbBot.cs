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
                if (this.PlayerScores.YahtzeeValidation(dice) && !this.PlayerScores.yahtzeeScored)
                {
                    this.PlayerScores.YahtzeeSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Yahtzee, scoring " +
                                    PlayerScores.yahtzee + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (this.PlayerScores.LargeStraightValidation(dice) && !this.PlayerScores.largeStraightScored)
                {
                    this.PlayerScores.LargeStraightSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Large Straight, scoring " +
                                    PlayerScores.largeStraight + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (this.PlayerScores.SmallStraightValidation(dice) && !this.PlayerScores.smallStraightScored)
                {
                    this.PlayerScores.SmallStraightSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Small Straight, scoring " +
                                    PlayerScores.smallStraight + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (this.PlayerScores.FourKindValidation(dice) && !this.PlayerScores.fourOfAKindScored)
                {
                    this.PlayerScores.FourOfAKindSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Four of a Kind, scoring " +
                                    PlayerScores.fourOfAKind + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (this.PlayerScores.ThreeKindValidation(dice) && !this.PlayerScores.threeOfAKindScored)
                {
                    this.PlayerScores.ThreeOfAKindSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Three of a Kind, scoring " +
                                    PlayerScores.threeOfAKind + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (this.PlayerScores.FullHouseValidation(dice) && !this.PlayerScores.fullHouseScored)
                {
                    this.PlayerScores.FullHouseSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Full House, scoring " +
                                    PlayerScores.fullHouse + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.sixesScored && this.PlayerScores.sixes > 0)
                {
                    this.PlayerScores.SixesSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Sixes, scoring " +
                                    PlayerScores.sixes + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.fivesScored && this.PlayerScores.fives > 0)
                {
                    this.PlayerScores.FivesSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Fives, scoring " +
                                    PlayerScores.fives + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.foursScored && this.PlayerScores.fours > 0)
                {
                    this.PlayerScores.FoursSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Fours, scoring " +
                                    PlayerScores.fours + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.threesScored && this.PlayerScores.threes > 0)
                {
                    this.PlayerScores.ThreesSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Threes, scoring " +
                                    PlayerScores.threes + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.twosScored && this.PlayerScores.twos > 0)
                {
                    this.PlayerScores.TwosSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Twos, scoring " + PlayerScores.twos +
                                    " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.acesScored && this.PlayerScores.aces > 0)
                {
                    this.PlayerScores.AcesSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Aces, scoring " + PlayerScores.aces +
                                    " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.chanceScored)
                {
                    this.PlayerScores.ChanceSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Chance, scoring " +
                                    PlayerScores.chance + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.acesScored)
                {
                    this.PlayerScores.AcesSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Aces, scoring " + PlayerScores.aces +
                                    " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.twosScored)
                {
                    this.PlayerScores.TwosSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Twos, scoring " + PlayerScores.twos +
                                    " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.threesScored)
                {
                    this.PlayerScores.ThreesSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Threes, scoring " +
                                    PlayerScores.threes + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.foursScored)
                {
                    this.PlayerScores.FoursSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Fours, scoring " +
                                    PlayerScores.fours + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.fivesScored)
                {
                    this.PlayerScores.FivesSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Fives, scoring " +
                                    PlayerScores.fives + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.sixesScored)
                {
                    this.PlayerScores.SixesSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Sixes, scoring " +
                                    PlayerScores.sixes + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.threeOfAKindScored)
                {
                    this.PlayerScores.ThreeOfAKindSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Three of a Kind, scoring " +
                                    PlayerScores.threeOfAKind + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.fourOfAKindScored)
                {
                    this.PlayerScores.FourOfAKindSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Four of a Kind, scoring " +
                                    PlayerScores.fourOfAKind + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.fullHouseScored)
                {
                    this.PlayerScores.FullHouseSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Full House, scoring " +
                                    PlayerScores.fullHouse + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.smallStraightScored)
                {
                    this.PlayerScores.SmallStraightSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Small Straight, scoring " +
                                    PlayerScores.smallStraight + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.largeStraightScored)
                {
                    this.PlayerScores.LargeStraightSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Large Straight, scoring " +
                                    PlayerScores.largeStraight + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
                }
                else if (!this.PlayerScores.yahtzeeScored)
                {
                    this.PlayerScores.YahtzeeSelected(dice, true);
                    MessageBox.Show("The Bot has made a decision. It has selected Yahtzee, scoring " +
                                    PlayerScores.yahtzee + " points, and " +
                                    "now has " + PlayerScores.totalScore + " points.");
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
