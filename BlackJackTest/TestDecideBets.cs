using BlackJackClasses;
using System.Xml;
using Moq;
using GameInterface;

namespace BlackJackTest;

[TestClass]
public class TestDecideBets
{
    public Game game;
    public TestDecideBets()
    {
        var mockView = new Mock<IView>();
        game = new Game(mockView.Object);
    }

    [TestMethod]
    public void DecideBets_DealerBlackJackHumanNot_WinnerIsDealer()
    {
        // Arange
        Dealer dealer = new DealerBuilder()
            .WithBlackJack()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder()
            .With16()
            .Build();

        // Act
        BetResult betResult = game.DecidePersonalBet(dealer, player);

        // Assert
        Assert.AreEqual(dealer, betResult.Winner);
        Assert.AreEqual(player, betResult.Loser);
    }

    [TestMethod]
    public void DecideBets_DealerBlackJackHumanAlso_isTie()
    {
        // Arange
        Dealer dealer = new DealerBuilder()
            .WithBlackJack()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder()
            .WithBlackJack()
            .Build();

        // Act
        BetResult betResult = game.DecidePersonalBet(dealer, player);

        // Assert
        Assert.IsTrue(betResult.Tie);
    }

    [TestMethod]
    public void DecideBets_DealerBustsHumanAlso_isTie()
    {
        // Arange
        Dealer dealer = new DealerBuilder()
            .ThatBusts()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder()
            .ThatBusts()
            .Build();

        // Act
        BetResult betResult = game.DecidePersonalBet(dealer, player);

        // Assert
        Assert.IsTrue(betResult.Tie);
    }

    [TestMethod]
    public void DecideBets_DealerBustsHumanBlackJack_WinnerIsHuman()
    {
        // Arange
        Dealer dealer = new DealerBuilder()
            .ThatBusts()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder()
            .WithBlackJack()
            .Build();

        // Act
        BetResult betResult = game.DecidePersonalBet(dealer, player);

        // Assert
        Assert.AreEqual(player, betResult.Winner);
        Assert.AreEqual(dealer, betResult.Loser);
    }

    [TestMethod]
    public void DecideBets_DealerBustsHumanNot_WinnerIsHuman()
    {
        // Arange
        Dealer dealer = new DealerBuilder()
            .ThatBusts()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder()
            .With16()
            .Build();

        // Act
        BetResult betResult = game.DecidePersonalBet(dealer, player);

        // Assert
        Assert.AreEqual(player, betResult.Winner);
        Assert.AreEqual(dealer, betResult.Loser);
    }

    [TestMethod]
    public void DecideBets_Dealer16HumanBusts_WinnerIsDealer()
    {
        // Arange
        Dealer dealer = new DealerBuilder()
            .With16()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder()
            .ThatBusts()
            .Build();

        // Act
        BetResult betResult = game.DecidePersonalBet(dealer, player);

        // Assert
        Assert.AreEqual(dealer, betResult.Winner);
        Assert.AreEqual(player, betResult.Loser);
    }

    [TestMethod]
    public void DecideBets_DealerLeq21HumanLower_WinnerIsDealer()
    {
        // Arange
        Dealer dealer = new DealerBuilder()
            .With16()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder()
            .With13()
            .Build();

        // Act
        BetResult betResult = game.DecidePersonalBet(dealer, player);

        // Assert
        Assert.AreEqual(dealer, betResult.Winner);
        Assert.AreEqual(player, betResult.Loser);
    }

    [TestMethod]
    public void DecideBets_DealerLeq21HumanHigher_WinnerIsPlayer()
    {
        // Arange
        Dealer dealer = new DealerBuilder()
            .With16()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder()
            .With21()
            .Build();

        // Act
        BetResult betResult = game.DecidePersonalBet(dealer, player);

        // Assert
        Assert.AreEqual(player, betResult.Winner);
        Assert.AreEqual(dealer, betResult.Loser);
    }

    [TestMethod]
    public void DecideBets_DealerLeq21HumanEqual_isTie()
    {
        // Arange
        Dealer dealer = new DealerBuilder()
            .With16()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder()
            .With16()
            .Build();

        // Act
        BetResult betResult = game.DecidePersonalBet(dealer, player);

        // Assert
        Assert.IsTrue(betResult.Tie);
    }
}
