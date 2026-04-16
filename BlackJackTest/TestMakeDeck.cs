using BlackJackClasses;

namespace BlackJackTest
{
    [TestClass]
    public sealed class TestMakeDeck
    {
        static Deck deck = new Deck();

        [TestMethod]
        public void TestInitializeCards_length_is52()
        {
            // Arange
            int expectedAnswer = 52;

            // Assert
            Assert.HasCount(expectedAnswer, deck.Cards);
        }

        [TestMethod]
        public void TestInitializeCards_suits_is4()
        {
            // Arange
            int expectedAnswer = 4;
            List<string> suits = new();

            // Act
            foreach (Card card in deck.Cards)
            {
                if (!suits.Contains(card.Suit))
                {
                    suits.Add(card.Suit);
                }
            }

            // Assert
            Assert.HasCount(expectedAnswer, suits);
        }

        [TestMethod]
        public void TestInitializeCards_maxValue_is11()
        {
            // Arange
            int expectedAnswer = 11;
            int maxValue = 0;

            // Act
            foreach (Card card in deck.Cards)
            {
                maxValue = Math.Max(maxValue, card.Value);
            }

            // Assert
            Assert.AreEqual(expectedAnswer, maxValue);
        }

        [TestMethod]
        public void TestInitializeCards_Distincts_isLength()
        {
            // Assert
            Assert.HasCount(deck.Cards.Distinct().Count(), deck.Cards);
        }
    }
}
