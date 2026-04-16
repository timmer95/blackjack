using BlackJackClasses;
using System.Security.Cryptography;

namespace BlackJackTest;


public class ZoFoeilelijkTestTransferBetMoney // This class really test nothing interesting
{
    // Only bet transfer with manual BlackJacks, no real comparisons of Hands
    Dealer? localDealer;
    HumanPlayer? localPlayer;

    [TestInitialize]
    public void BeforeEach()
    {
        localDealer = new Dealer(1000);
        localPlayer = new HumanPlayer(1, 500);
        localPlayer.SetBet(200);
    }

    
    public void TestCollectBet_DealerWins_isPlayersBet()
    {
        // Arange
        int expectedDealerCash = 1200;
        int expectedPlayerCash = 300;

        // Act
        localDealer.CollectBet(localPlayer.PayMoney(isBlackJack: false));

        // Assert
        Assert.AreEqual(expectedDealerCash, localDealer.Cash);
        Assert.AreEqual(expectedPlayerCash, localPlayer.Cash);
    }

    
    public void TestCollectBet_DefaultNoBlackJack_isSameResult()
    {
        // Arange
        localDealer.CollectBet(localPlayer.PayMoney(isBlackJack: false));
        int localDealerCashBJfalse = localDealer.Cash;

        // Arange
        Dealer localDealer2 = new Dealer(1000);
        HumanPlayer localPlayer2 = new HumanPlayer(1, 500);
        localPlayer2.SetBet(200);

        // Act
        localDealer2.CollectBet(localPlayer2.PayMoney());
        int localDealerCashDefault = localDealer2.Cash;

        // Assert
        Assert.AreEqual(localDealerCashBJfalse, localDealerCashDefault);
    }


    
    public void TestCollectBet_PlayerWins_isPlayersBet()
    {
        // Arange
        int expectedDealerCash = 800;
        int expectedPlayerCash = 700;

        // Act
        localPlayer.CollectBet(localDealer.PayMoney(localPlayer.Bet, isBlackJack: false));

        // Assert
        Assert.AreEqual(expectedDealerCash, localDealer.Cash);
        Assert.AreEqual(expectedPlayerCash, localPlayer.Cash);
    }


    
    public void TestCollectBet_DealerWinsBlackJack_isPlayersBet32()
    {
        // Arange
        int expectedDealerCash = 1300;
        int expectedPlayerCash = 200;

        // Act
        localDealer.CollectBet(localPlayer.PayMoney(isBlackJack: true));

        // Assert
        Assert.AreEqual(expectedDealerCash, localDealer.Cash);
        Assert.AreEqual(expectedPlayerCash, localPlayer.Cash);
    }

    
    public void TestCollectBet_PlayerWinsBlackJack_isPlayersBet32()
    {
        // Arange
        int expectedDealerCash = 700;
        int expectedPlayerCash = 800;

        // Act
        localPlayer.CollectBet(localDealer.PayMoney(localPlayer.Bet, isBlackJack: true));

        // Assert
        Assert.AreEqual(expectedDealerCash, localDealer.Cash);
        Assert.AreEqual(expectedPlayerCash, localPlayer.Cash);
    }
}
