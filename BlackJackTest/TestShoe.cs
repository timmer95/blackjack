using BlackJackClasses;
using Microsoft.CodeCoverage.Core.Reports.Coverage;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace BlackJackTest
{
    [TestClass]
    public sealed class TestShoe
    {
        static Deck deck1 = new Deck();
        static Deck deck2 = new Deck();
        static Deck[] twoDecks = { deck1, deck2 };
        static Shoe shoe1 = new Shoe(twoDecks);
        static Shoe shoe2 = new Shoe(twoDecks);
        static Shoe shoeForShuffle = new Shoe(twoDecks);

        [TestMethod]
        public void TestInitializeShoe_length_is104()
        {
            // Arange
            int expectedAnswer = 104;

            // Act & Assert
            Assert.HasCount(expectedAnswer, shoe1.stack);
        }

        [TestMethod]
        public void TestInitializeShoe_facedUp_isFalse()
        {
            // Assert
            Assert.IsFalse(shoe1.stack[0].FacedUp);
        }

        [TestMethod]
        public void TestDrawCard_firstCardDrawn_isLastCardAdded()
        {
            // Arrange
            Card expectedAnswer = deck2.Cards[deck2.Cards.Length - 1];

            // Act
            Card drawnCard = shoe1.DrawCard();

            // Assert
            Assert.AreEqual(expectedAnswer, drawnCard);
            Assert.AreSame(expectedAnswer, drawnCard); // is this what you want? that by drawing a card, you get the reference to that card? I think so? 
        }

        [TestMethod]
        public void TestDrawCards_untilEmpty_isErr()
        {
            // Arrange
            void KeepDrawing()
            {
                int n = 0;
                while (n <= shoe2.stack.Length + 1)
                {
                    Card drawnCard = shoe2.DrawCard();
                    n++;
                }
            }
            
            // Act & Assert
            Assert.ThrowsExactly<InvalidOperationException>(() => KeepDrawing());
            bool isShoeEmpty = shoe2.IsEmpty();
            Assert.IsTrue(isShoeEmpty);
        }

        [TestMethod]
        public void TestShuffleCards() // Risky method! the shuffle can result in the same card.....
        {
            // Arange
            Card firstCard1 = shoeForShuffle.stack[0];

            // Act
            shoeForShuffle.Shuffle();
            Card firstCard2 = shoeForShuffle.stack[0];
            shoeForShuffle.Shuffle();
            Card firstCard3 = shoeForShuffle.stack[0]; 

            // Assert
            Assert.AreNotSame(firstCard1, firstCard2); // Same i.o. Equals bc there can be multiple equal cards in the same stack, but not same cards
            Assert.AreNotSame(firstCard2, firstCard3);
        }
    } 
}
