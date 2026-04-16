using BlackJackClasses;
using Microsoft.CodeCoverage.Core.Reports.Coverage;
using System.Runtime.CompilerServices;

namespace BlackJackTest
{
    [TestClass]
    public sealed class TestExtendHand
    {

        [TestMethod]
        public void TestInitializeHand_lenght_is0()
        {
            // Arange
            Hand hand = new Hand();
            int expectedAnswer = 0;

            // Act & Assert
            Assert.HasCount(expectedAnswer, hand.Cards);
        }

        [TestMethod]
        public void TestAddCard_length_is5()
        {
            // Arange
            Hand hand = new Hand();
            string[] suits = { "Clubs", "Spades", "Hearts", "Diamonds" };
            Random r = new Random();
            int takeCardTimes = 5;
            int timesCardTaken = 0;
            int expectedAnswer = takeCardTimes;

            // Act
            while (timesCardTaken < takeCardTimes)
            {
                hand.AddCard(new Card(r.Next(2, 10), suits[r.Next(0, suits.Length - 1)]));
                timesCardTaken++;
            }
            
            // Assert
            Assert.HasCount(expectedAnswer, hand.Cards);
        }
    } 
}
