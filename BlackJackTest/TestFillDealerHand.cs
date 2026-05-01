using BlackJackClasses;

namespace BlackJackTest;

[TestClass]
public class TestFillDealerHand
{
    static readonly Card spadesKing = new Card(10, "Spades", "King");
    static readonly Card diamondFour = new Card(4, "Diamonds");
    static readonly Card heartsSeven = new Card(7, "Hearts");
    static readonly Card clubsNine = new Card(9, "Clubs");

    int dealerStandsAt = 16;

    static Deck deck1 = new Deck();
    static Deck deck2 = new Deck();
    static Deck[] twoDecks = { deck1, deck2 };
    static Shoe shoe1 = new Shoe(twoDecks, doShuffle:true);

    Dealer dealer;

    public TestFillDealerHand()
    {
        dealer = new Dealer(1000);
    }

    [TestMethod]
    public void TestFillHand_HighEnough_isNoAdditionalCards()
    {
        // Arange
        
        dealer.AddToHand(spadesKing, heartsSeven);
        int expectedAnswer = 2;

        // Act
        dealer.FillHand(shoe1, dealerStandsAt);

        // Assert
        Assert.HasCount(expectedAnswer, dealer.Hand.Cards);
    }

    [TestMethod]
    public void TestFillHand_FinalValue_isHigherThanDealerStandsAt_rpt5()
    {
        for (int n = 1; n <= 5; n++)
        {
            // Arange
            Dealer dealer = new Dealer(1000);
            dealer.AddToHand(diamondFour, heartsSeven);

            // Act
            dealer.FillHand(shoe1, dealerStandsAt);

            // Assert
            Assert.IsGreaterThanOrEqualTo(dealerStandsAt, dealer.Hand.Value);
            Console.WriteLine(dealer.Hand);
        } 
    }
    [TestMethod]
    public void TestFillHand_ExactlyStandsAt_isNoAdditionalCards()
    {
        // Arange
        Dealer dealer = new Dealer(1000);
        dealer.AddToHand(clubsNine, heartsSeven);
        int expectedAnswer = 2;

        // Act
        dealer.FillHand(shoe1, dealerStandsAt);

        // Assert
        Assert.HasCount(expectedAnswer, dealer.Hand.Cards);
    }
}
