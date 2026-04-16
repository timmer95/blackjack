using BlackJackClasses;

namespace BlackJackTest
{
    [TestClass]
    public sealed class TestCard
    {
        static readonly Card kingSpades1 = new Card(10, "Spades", "King");
        static readonly Card diamondFour = new Card(4, "Diamonds");

        [TestMethod]
        public void TestInitializeCard_noName_isValue()
        {
            // Assert
            Assert.AreEqual(diamondFour.Name, diamondFour.Value.ToString());
        }

        [TestMethod]
        public void TestCard_FacedUp_isFalse()
        {
            // Assert
            Assert.IsFalse(kingSpades1.FacedUp);
        }

        [TestMethod]
        public void TestSame_TwoSimilarCards_isNotSameObj()
        {
            // Arange
            Card kingSpades2 = new Card(10, "Spades", "King");

            // Assert
            Assert.AreNotSame(kingSpades2, kingSpades1); 
        }

        [TestMethod]
        public void TestSame_TwoDifferentCards_isNotSameObj()
        {
            // Arange
            Card queenSpades = new Card(10, "Spades", "Queen");

            // Assert
            Assert.AreNotSame(queenSpades, kingSpades1);
        }

        [TestMethod]
        public void TestSame_DuplicationOfCard_isSameObj()
        {
            // Arange
            Card queenSpades = new Card(10, "Spades", "Queen");
            Card queenSpades2 = queenSpades;

            // Assert
            Assert.AreSame(queenSpades, queenSpades2);
        }

        [TestMethod]
        public void TestEquals_TwoSimilarCards_isEqual()
        {
            // Arange
            Card kingSpades2 = new Card(10, "Spades", "King");

            // Assert
            Assert.AreEqual(kingSpades2, kingSpades1);
        }

        [TestMethod]
        public void TestEquals_TwoDifferentCards_isNotEqual()
        {
            // Arange
            Card queenSpades = new Card(10, "Spades", "Queen");

            // Assert
            Assert.AreNotEqual(queenSpades, kingSpades1);
        }
    }
}
