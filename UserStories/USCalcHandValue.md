# User Story - Calculate Hand Value

## Story
As a Player, I want to know the value of my Hand, so that I can decide on my next action.

## Acceptance criteria
The value of a hand is the sum of all card's values
The value of Ace is considered 11 when total does not exceed 21
The value of Ace is considered 1 when total would exceed 21 upon consideration of Ace as 11
The value of a Hand with no Cards is 0
An initial Hand of 21 is a BlackJack
Later on, a Hand of 21 is not a BlackJack

When a hand has two Aces and one Seven, one Ace must have value of 11 and the other of 1
When a hand has two Aces, one Seven and one Four, both Aces must have value of 1