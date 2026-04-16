# User Story - Decide Bets

## Story
As a Casino, I want know who won the bet, so I can exchange money later on.

## Acceptance criteria
If the dealer has BlackJack and the HumanPlayer not, the Dealer wins
If the dealer has blackjack but a player also has blackjack, it's a tie

If the dealer busts and the HumanPlayer does too, it's a tie
If the dealer busts and the Humanplayer had BlackJack, the HumanPlayer wins
If the dealer busts but the Humanplayer busts as well, it's a tie

If the dealer has <= 21 and no BlackJack and the HumanPlayer has lower Hand value, the Dealer wins
If the dealer has <= 21 and no BlackJack and the HumanPlayer has higher Hand value, the HumanPlayer wins
If the dealer has <= 21 and no BlackJack and the HumanPlayer has an equal Hand value, it's a tie
