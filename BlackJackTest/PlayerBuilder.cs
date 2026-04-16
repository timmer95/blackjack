using System;
using System.Collections.Generic;
using System.Text;
using BlackJackClasses;

namespace BlackJackTest
{
    // Generic fluent builder base using CRTP so methods return the concrete builder type.
    internal abstract class PlayerBuilderBase<TBuilder, TPlayer>
        where TBuilder : PlayerBuilderBase<TBuilder, TPlayer>
        where TPlayer : Player
    {
        protected TPlayer player;

        static readonly Card spadesKing = new Card(10, "Spades", "King");
        static readonly Card spadesSix = new Card(6, "Spades");
        static readonly Card diamondFour = new Card(4, "Diamonds");
        static readonly Card clubsAce = new Card(11, "Clubs", "Ace");
        static readonly Card heartsSeven = new Card(7, "Hearts");

        protected PlayerBuilderBase(TPlayer player)
        {
            this.player = player;
        }

        public TBuilder WithBlackJack()
        {
            player.AddToHand(spadesKing, clubsAce);
            player.Hand.CalcInitialValue();
            return (TBuilder)this;
        }

        public TBuilder ThatBusts()
        {
            player.AddToHand(spadesKing, heartsSeven, spadesSix);
            player.Hand.CalcValue();
            return (TBuilder)this;
        }

        public TBuilder With21()
        {
            player.AddToHand(spadesKing, heartsSeven, diamondFour);
            player.Hand.CalcValue();
            return (TBuilder)this;
        }

        public TBuilder With16()
        {
            player.AddToHand(spadesKing, spadesSix);
            player.Hand.CalcValue();
            return (TBuilder)this;
        }

        public TBuilder With13()
        {
            player.AddToHand(heartsSeven, spadesSix);
            player.Hand.CalcValue();
            return (TBuilder)this;
        }

        public TPlayer Build()
        {
            return player;
        }
    }

    // Concrete non-generic PlayerBuilder with defaultCash for tests that need a plain Player
    internal class PlayerBuilder : PlayerBuilderBase<PlayerBuilder, Player>
    {
        public static int defaultCash = 1000; // can be changed with object initializer

        public PlayerBuilder(int? cash = null) : base(new Player(cash ?? PlayerBuilder.defaultCash)) { }
    }

    internal class DealerBuilder : PlayerBuilderBase<DealerBuilder, Dealer>
    {
        public DealerBuilder(int? cash = null) : base(new Dealer(cash ?? PlayerBuilder.defaultCash)) { }
    }

    internal class HumanPlayerBuilder : PlayerBuilderBase<HumanPlayerBuilder, HumanPlayer>
    {
        public HumanPlayerBuilder(int? cash = null) : base(new HumanPlayer(1, cash ?? PlayerBuilder.defaultCash)) { }

        public HumanPlayerBuilder WithBet(int money)
        {
            player.Bet = money;
            return this;
        }
    }
}
