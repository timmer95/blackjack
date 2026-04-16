using BlackJackClasses;
using Moq;
using GameInterface;

namespace BlackJackTest;

[TestClass]
public class TestPlaceBets
{
    [TestMethod]
    public void TestPlaceBets_AsksForUserInput()
    {
        // Arrange
        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.DisplayMessage(It.IsAny<string>()));
        Game game = new Game(mockView.Object) { NPlayers = 1};
        game.SetUp();

        // Act
        game.PlaceBets();

        // Assert
        mockView.Verify(vw => vw.DisplayMessage(It.IsRegex(".*bet.*")));
    }

    [TestMethod]
    public void TestPlaceBets_BetIs50_isStored()
    {
        // Arrange
        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.ReadInput()).Returns("50");

        Game game = new Game(mockView.Object) { NPlayers = 1 }; 
        game.SetUp();
        int expectedAnswer = 50;

        // Act
        game.PlaceBets();

        // Assert
        Assert.AreEqual(expectedAnswer, game.ActivePlayers![0].Bet);
    }

    [TestMethod]
    public void TestPlaceBets_BetHigherThanCash_isErr()
    {
        // Arrange
        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.ReadInput()).Returns("50");
        Game game = new Game(mockView.Object) { NPlayers = 1, PlayerCash = 40 };
        game.SetUp();

        // Act
        void act()
        {
            game.PlaceBets();
        }

        // Assert
        Assert.ThrowsExactly<InvalidOperationException>(act);
    }

    [TestMethod]
    public void TestPlaceBets_BetExactlyCashIfBlackJack_is50()
    {
        // Arrange
        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.ReadInput()).Returns("50");
        Game game = new Game(mockView.Object) { NPlayers = 1, PlayerCash = 75 };
        game.SetUp();
        int expectedAnswer = 50;

        // Act
        game.PlaceBets();

        // Assert
        Assert.AreEqual(expectedAnswer, game.ActivePlayers![0].Bet);
    }

    [TestMethod]
    public void TestPlaceBets_3Players_is3Bets()
    {
        // Arrange
        int bet1 = 50;
        int bet2 = 30;
        int bet3 = 100;

        var mockView = new Mock<IView>();
        mockView.SetupSequence(vw => vw.ReadInput()).Returns(bet1.ToString()).Returns(bet2.ToString()).Returns(bet3.ToString());

        Game game = new Game(mockView.Object){NPlayers = 3};
        game.SetUp();

        // Act
        game.PlaceBets();

        // Assert
        Assert.AreEqual(bet1, game.ActivePlayers![0].Bet);
        Assert.AreEqual(bet2, game.ActivePlayers[1].Bet);
        Assert.AreEqual(bet3, game.ActivePlayers[2].Bet);
    }
}
