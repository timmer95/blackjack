using BlackJackClasses;
using Moq;
using GameInterface;
using Delegates;

namespace BlackJackTest;

[TestClass]
public class TestPlaceBets
{

    [TestMethod]
    public void TestPlaceBets_AsksForUserInput()
    {
        // Arrange
        var mockView = new Mock<IView>();
        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();

        Player player = game.ActivePlayers!.First();

        mockView.Setup(vw => vw.GetValidatedInput<int>(It.IsAny<string>(), It.IsAny<Validator<int>>())).Returns(50);

        // Act
        game.PlaceBets();

        // Assert
        mockView.Verify(vw => vw.GetValidatedInput<int>(It.IsRegex(".*bet.*"), It.IsAny<Validator<int>>())); // 
    }

    [TestMethod]
    public void TestPlaceBets_BetIs50_isStored()
    {
        // Arrange
        var mockView = new Mock<IView>();
        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();
        Player player = game.ActivePlayers!.First();
        mockView.Setup(vw => vw.GetValidatedInput<int>(It.IsAny<string>(), It.IsAny<Validator<int>>())).Returns(50);
        int expectedAnswer = 50;

        // Act
        game.PlaceBets();

        // Assert
        Assert.AreEqual(expectedAnswer, game.ActivePlayers![0].Bet);
    }

    //[TestMethod]
    //public void TestPlaceBets_BetHigherThanCash_isErr() // This must be on player, not here
    //{
    //    // Arrange
    //    var mockView = new Mock<IView>();
    //    mockView.Setup(vw => vw.ReadInput()).Returns("50");

    //    Game game = new Game(mockView.Object) { NPlayers = 1, PlayerCash = 40 };
    //    game.SetUp();

    //    // Act
    //    void act()
    //    {
    //        game.ActivePlayers![0].SetBet(50);
    //    }

    //    // Assert
    //    Assert.ThrowsExactly<InvalidOperationException>(act);
    //}

    [TestMethod]
    public void TestPlaceBets_BetExactlyCashIfBlackJack_is50()
    {
        // Arrange
        var mockView = new Mock<IView>();
        Game game = new Game(mockView.Object) { NPlayers = 1, PlayerCash = 75 };
        game.SetUp();
        Player player = game.ActivePlayers!.First();
        mockView.Setup(vw => vw.GetValidatedInput<int>(It.IsAny<string>(), It.IsAny<Validator<int>>())).Returns(50);
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
        mockView.SetupSequence(vw => vw.GetValidatedInput<int>(It.IsAny<string>(), It.IsAny<Validator<int>>())).Returns(bet1).Returns(bet2).Returns(bet3);

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
