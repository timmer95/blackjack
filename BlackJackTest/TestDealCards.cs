using BlackJackClasses;

namespace BlackJackTest;

[TestClass]
public class TestDealCards
{
    static Deck deck1 = new Deck();
    static Deck deck2 = new Deck();
    static Deck[] twoDecks = { deck1, deck2 };
    static Shoe shoe1 = new Shoe(twoDecks, doShuffle:true);

    Dealer dealer1;
    HumanPlayer player1;
    HumanPlayer player2;

    HumanPlayer[] players;

    [TestInitialize]
    public void BeforeEach()
    {
        dealer1 = new Dealer(600);
        player1 = new HumanPlayer(1, 500);
        player2 = new HumanPlayer(2, 750);

        players = new HumanPlayer[] { player1, player2 };
    }

    [TestMethod]
    public void TestDealCards_everyonesHand_isLength2()
    {
        // Arange
        int expectedAnswer = 2;

        // Act
        dealer1.DealCards(shoe1, players, 2);

        // Assert
        foreach (Player player in new Player[] { player1, player2, dealer1})
        {
            Console.WriteLine(player.Hand);
            Assert.HasCount(expectedAnswer, player.Hand.Cards);
        }
    }

    [TestMethod]
    public void TestDealCards_DealersCard2_isFacedDown() // Deze doet soms een foutmelding. hoe kan dat
    {
        // Act
        dealer1.DealCards(shoe1, players, 2);

        // Assert
        Assert.IsTrue(dealer1.Hand.Cards[0].FacedUp);
        Assert.IsFalse(dealer1.Hand.Cards[1].FacedUp);
    }

    [TestMethod]
    public void TestDealCards_PlayersCards_isFacedUp()
    {
        // Act
        dealer1.DealCards(shoe1, players, 2);

        // Assert
        Assert.IsTrue(player1.Hand.Cards[0].FacedUp);
        Assert.IsTrue(player2.Hand.Cards[0].FacedUp);
        Assert.IsTrue(player1.Hand.Cards[1].FacedUp);
        Assert.IsTrue(player2.Hand.Cards[1].FacedUp);

    }
}
