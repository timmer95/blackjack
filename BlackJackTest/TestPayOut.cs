using BlackJackClasses;
using System.Security.Cryptography;
using Moq;
using GameInterface;

namespace BlackJackTest;

[TestClass]
public class TestPayOut // This class really test nothing interesting
{
    // Only bet transfer with manual BlackJacks, no real comparisons of Hands
    int dealerInitCash = 1000;
    int playerInitCash = 500;
    int playerBet = 200;

    Game game;

    public TestPayOut()
    {
        var mockView = new Mock<IView>();
        game = new Game(mockView.Object);
    }


    [TestMethod]
    public void TestPayOut_DealerWins_isPlayersBet()
    {
        // Arange
        Dealer dealer = new DealerBuilder(dealerInitCash)
            .With21()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder(playerInitCash)
            .WithBet(playerBet)
            .With16()
            .Build();

        int expectedDealerCash = 1200;
        int expectedPlayerCash = 300;
        BetResult betResult = new BetResult(dealer, player);
        
        // Act
        game.PayOut(betResult, player);

        // Assert
        Assert.AreEqual(expectedDealerCash, dealer.Cash);
        Assert.AreEqual(expectedPlayerCash, player.Cash);
    }

    [TestMethod]
    public void TestPayOut_PlayerWins_isPlayersBet()
    {
        // Arange
        Dealer dealer = new DealerBuilder(dealerInitCash)
            .With13()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder(playerInitCash)
            .WithBet(playerBet)
            .With16()
            .Build();

        int expectedDealerCash = 800;
        int expectedPlayerCash = 700;
        BetResult betResult = new BetResult(player, dealer);

        // Act
        game.PayOut(betResult, player);

        // Assert
        Assert.AreEqual(expectedDealerCash, dealer.Cash);
        Assert.AreEqual(expectedPlayerCash, player.Cash);
    }


    [TestMethod]
    public void TestPayOut_DealerWinsBlackJack_isPlayersBet32()
    {
        // Arange
        Dealer dealer = new DealerBuilder(dealerInitCash)
            .WithBlackJack()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder(playerInitCash)
            .WithBet(playerBet)
            .With16()
            .Build();

        int expectedDealerCash = 1300;
        int expectedPlayerCash = 200;
        BetResult betResult = new BetResult(dealer, player);

        // Act
        game.PayOut(betResult, player); 

        // Assert
        Assert.AreEqual(expectedDealerCash, dealer.Cash);
        Assert.AreEqual(expectedPlayerCash, player.Cash);
    }

    [TestMethod]
    public void TestPayOut_PlayerWinsBlackJack_isPlayersBet32()
    {
        // Arange
        Dealer dealer = new DealerBuilder(dealerInitCash)
            .With16()
            .Build();
        HumanPlayer player = new HumanPlayerBuilder(playerInitCash)
            .WithBet(playerBet)
            .WithBlackJack()
            .Build();

        int expectedDealerCash = 700;
        int expectedPlayerCash = 800;
        BetResult betResult = new BetResult(player, dealer);

        // Act
        game.PayOut(betResult, player); 

        // Assert
        Assert.AreEqual(expectedDealerCash, dealer.Cash);
        Assert.AreEqual(expectedPlayerCash, player.Cash);
    }
}
