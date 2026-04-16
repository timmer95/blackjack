using BlackJackClasses;

namespace BlackJackTest;

[TestClass]
public class TestCalcHandValue
{
    static readonly Card spadesKing = new Card(10, "Spades", "King");
    static readonly Card diamondFour = new Card(4, "Diamonds");
    static readonly Card clubsAce = new Card(11, "Clubs", "Ace");
    static readonly Card diamondAce = new Card(11, "Diamond", "Ace");
    static readonly Card heartsSeven = new Card(7, "Hearts");

    static Hand handEmpty = new Hand();


    [TestMethod]
    public void TestCalcValue_noCards_is0()
    {
        // Arange
        int expectedAnswer = 0;

        // Act
        int value = handEmpty.CalcValue();

        // Assert
        Assert.AreEqual(expectedAnswer, value);
    }

    [TestMethod]
    public void TestCalcValue_Sum_is14()
    {
        // Arange
        Hand localHand = new Hand([spadesKing, diamondFour]);
        int expectedAnswer = 14;

        // Act
        int value = localHand.CalcValue();

        // Assert
        Assert.AreEqual(expectedAnswer, value);
    }

    [TestMethod]
    public void TestCalcValue_wAceAs11_is15()
    {
        // Arange
        Hand localHand = new Hand([clubsAce, diamondFour]);
        int expectedAnswer = 15;

        // Act
        int value = localHand.CalcValue();

        // Assert
        Assert.AreEqual(expectedAnswer, value);
    }

    [TestMethod]
    public void TestCalcValue_wAceAs11_is21notBJ()
    {
        // Arange
        Hand localHand = new Hand([clubsAce, spadesKing]);
        int expectedAnswer = 21;

        // Act
        int value = localHand.CalcValue();

        // Assert
        Assert.AreEqual(expectedAnswer, value);
        Assert.IsFalse(localHand.IsBlackJack);
    }
	
    [TestMethod]
    public void TestCalcInitialValue_w21_isBlackJack()
    {
        // Arange
        Hand localHand = new Hand([clubsAce, spadesKing]);
        int expectedAnswer = 21;

        // Act
        int value = localHand.CalcInitialValue();

        // Assert
        Assert.AreEqual(expectedAnswer, value);
        Assert.IsTrue(localHand.IsBlackJack);
    }

    [TestMethod]
    public void TestCalcValue_wAceAs1_is15()
    {
        // Arange
        Hand localHand = new Hand([clubsAce, spadesKing, diamondFour]);
        int expectedAnswer = 15;

        // Act
        int value = localHand.CalcValue();

        // Assert
        Assert.AreEqual(expectedAnswer, value);
    }

    [TestMethod]
    public void TestCalcValue_wAceAs1_is12()
    {
        // Arange
        Hand localHand = new Hand([heartsSeven, diamondFour, clubsAce]);
        int expectedAnswer = 12;

        // Act
        int value = localHand.CalcValue();

        // Assert
        Assert.AreEqual(expectedAnswer, value);
    }

    [TestMethod]
    public void TestCalcValue_Value_isEqual()
    {
        // Arange
        Hand localHand = new Hand([heartsSeven, diamondFour, clubsAce]);
        int valueBefore = localHand.Value;

        // Act
        int valueReturned = localHand.CalcValue();
        int valueAfter = localHand.Value;

        // Assert
        Assert.IsGreaterThan(valueBefore, valueAfter);
        Assert.AreEqual(valueReturned, valueAfter);
    }

    [TestMethod]
    public void TestCalcValue_TwoAces_is19()
    {
        // Arange
        Hand localHand = new Hand([heartsSeven, diamondAce, clubsAce]);
        int expectedAnswer = 19;

        // Act
        int value = localHand.CalcValue();

        // Assert
        Assert.AreEqual(expectedAnswer, value);
    }

    [TestMethod]
    public void TestCalcValue_TwoAces_is13()
    {
        // Arange
        Hand localHand = new Hand([heartsSeven, diamondAce, clubsAce, diamondFour]);
        int expectedAnswer = 13;

        // Act
        int value = localHand.CalcValue();

        // Assert
        Assert.AreEqual(expectedAnswer, value);
    }
}
