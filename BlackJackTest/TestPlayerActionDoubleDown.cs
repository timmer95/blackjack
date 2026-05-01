using BlackJackClasses;
using GameInterface;
using Moq;

namespace BlackJackTest;

[TestClass]
public class TestPlayerActionDoubledown
{
    static readonly Card spadesKing = new Card(10, "Spades", "King");
    static readonly Card diamondFour = new Card(4, "Diamonds");
    static readonly Card spadesTwo = new Card(2, "Spades");
    static readonly Card heartsQueen = new Card(10, "Hearts", "Queen");
    static readonly Card fakeCardTooHigh = new Card(11, "Clubs", "Fake");


    [TestMethod]
    public void DoubleDown_wTwoCards_isStopTurnAfterOneCard()
    {
        // Arrange
        var mockView = new Mock<IView>();

        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();

        HumanPlayer player = new HumanPlayerBuilder()
            .With13()
            .Build();

        // Act
        bool continueTurn = game.ExecuteAction("double down", player);

        // Assert
        Assert.IsFalse(continueTurn);
    }

    [TestMethod]
    public void DoubleDown_wTwoCards_isThreeCards()
    {
        // Arrange
        var mockView = new Mock<IView>();

        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();

        HumanPlayer player = new HumanPlayerBuilder()
            .With13()
            .Build();

        // Act
        game.ExecuteAction("double down", player);

        // Assert
        Assert.HasCount(3, player.Hand.Cards);
    }

    [TestMethod]
    public void DoubleDown_wTwoCards_isAskForBetDoubling()
    {
        // Arrange
        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.DisplayMessage(It.IsAny<string>(), It.IsAny<bool>()));

        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();

        HumanPlayer player = new HumanPlayerBuilder()
            .With13()
            .WithBet(50)
            .Build();

        // Act
        game.ExecuteAction("double down", player);

        // Assert
        mockView.Verify(vw => vw.DisplayMessage(It.Is<string>(s => s.Contains("double bet")), It.IsAny<bool>()), Times.Once);
    }

    [TestMethod]
    public void DoubleDown_DoubleWith20_BetIs70()
    {
        // Arrange
        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.DisplayMessage(It.IsAny<string>(), It.IsAny<bool>()));
        mockView.Setup(vw => vw.ReadInput()).Returns("20");

        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();

        HumanPlayer player = new HumanPlayerBuilder()
            .With13()
            .WithBet(50)
            .Build();

        // Act
        game.ExecuteAction("double down", player);

        // Assert
        Assert.AreEqual(70, player.Bet);
    }

    [TestMethod]
    public void DoubleDown_DoubleWith60_isAskAgain()
    {
        // Arrange
        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.DisplayMessage(It.IsAny<string>(), It.IsAny<bool>()));
        mockView.Setup(vw => vw.ReadInput()).Returns("60");

        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();

        HumanPlayer player = new HumanPlayerBuilder()
            .With13()
            .WithBet(50)
            .Build();

        // Act
        game.ExecuteAction("double down", player);

        // Assert
        Assert.AreNotEqual(110, player.Bet);
    }
}
