using BlackJackClasses;
using Delegates;
using GameInterface;
using Moq;

namespace BlackJackTest;

[TestClass]
public class TestPlayerBasicAction
{
    static readonly Card spadesKing = new Card(10, "Spades", "King");
    static readonly Card diamondFour = new Card(4, "Diamonds");
    static readonly Card spadesTwo = new Card(2, "Spades");
    static readonly Card heartsQueen = new Card(10, "Hearts", "Queen");
    static readonly Card fakeCardTooHigh = new Card(11, "Clubs", "Fake");


    [TestMethod]
    public void AskUserActionChoice_Console_ProvidesUserWithOptions()
    {
        // Arrange
        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.GetValidatedInput<string>(It.IsAny<string>(), It.IsAny<Validator<string>>())).Returns("stand");

        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();

        // Act
        game.AskUserActionChoice();

        // Assert
        mockView.Verify(vw => vw.GetValidatedInput<string>(It.IsRegex(".*hit.*"), It.IsAny<Validator<string>>()));
        mockView.Verify(vw => vw.GetValidatedInput<string>(It.IsRegex(".*stand.*"), It.IsAny<Validator<string>>()));

    }

    [TestMethod]
    public void AskUserActionChoice_ChoosesHit_ActionIsHit()
    {
        // Arrange
        string actionOfChoice = "hit";

        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.GetValidatedInput<string>(It.IsAny<string>(), It.IsAny<Validator<string>>())).Returns(actionOfChoice);

        Game game = new Game(mockView.Object) { NPlayers = 1 };

        // Act
        string actionReturned = game.AskUserActionChoice();

        // Assert
        Assert.AreEqual(actionOfChoice, actionReturned);
    }

    [TestMethod]
    public void AskUserActionChoice_WrongOptions_KeepsAsking()
    {
        // Arrange
        string wrongAction = "make BlackJack";
        string correctAction = "stand";

        var mockView = new Mock<IView>();
        mockView.SetupSequence(vw => vw.GetValidatedInput<string>(It.IsAny<string>(), It.IsAny<Validator<string>>())).Returns(wrongAction).Returns(wrongAction).Returns(correctAction);
        Game game = new Game(mockView.Object) { NPlayers = 1 };
     
        // Act
        string actionReturend = game.AskUserActionChoice();

        // Assert
        Assert.AreNotEqual(wrongAction, actionReturend);
        Assert.AreEqual(correctAction, actionReturend);
    }

    [TestMethod]
    public void ExecuteAction_Hit_isExtraCard()
    {
        // Arrange
        string actionOfChoice = "hit";

        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.ReadInput()).Returns(actionOfChoice);

        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();

        HumanPlayer player = game.ActivePlayers!.First();
        player.AddToHand(spadesTwo, diamondFour); // Such low values, hit will never bust

        // Act
        bool turnContinues = game.ExecuteAction(actionOfChoice, player);

        // Assert
        Assert.IsTrue(turnContinues);
        Assert.HasCount(3, player.Hand.Cards);
    }

    [TestMethod]
    public void ExecuteAction_Stand_isEndTurn()
    {
        // Arrange
        string actionOfChoice = "stand";

        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.ReadInput()).Returns(actionOfChoice);

        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();
        HumanPlayer player = game.ActivePlayers!.First();
        player.AddToHand(spadesKing, heartsQueen);
        
        // Act
        bool turnContinues = game.ExecuteAction(actionOfChoice, player);

        // Assert
        Assert.IsFalse(turnContinues);
        Assert.HasCount(2, player.Hand.Cards);
    }

    [TestMethod]
    public void ExecuteAction_WrongAction_isNotExecuted()
    {
        // Arrange
        string actionOfChoice = "make BlackJack";

        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.ReadInput()).Returns(actionOfChoice);

        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();

        HumanPlayer player = game.ActivePlayers!.First();
        player.AddToHand(spadesKing, diamondFour);
        
        // Act
        bool turnContinues = game.ExecuteAction(actionOfChoice, player);

        // Assert
        Assert.IsFalse(turnContinues);
    }

    [TestMethod]
    public void ExecuteAction_Busts_isFalse()
    {
        // Arrange
        string actionOfChoice = "hit";

        var mockView = new Mock<IView>();
        mockView.Setup(vw => vw.ReadInput()).Returns(actionOfChoice);

        Game game = new Game(mockView.Object) { NPlayers = 1 };
        game.SetUp();

        HumanPlayer player = game.ActivePlayers!.First();
        player.AddToHand(spadesKing, fakeCardTooHigh); // is already 21, no one would hit but for the test they will
        
        // Act
        bool turnContinues = game.ExecuteAction(actionOfChoice, player);

        // Assert
        Assert.IsFalse(turnContinues);
    }
}
