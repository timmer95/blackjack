# User Story - Make Shoe

## Story
As a Dealer, I want to make a Shoe, so that there is a shuffled stack to draw cards from.

## Acceptance criteria
A Shoe cannot be created without at least one deck // taken care of by compiler
The Shoe has the number of cards of a deck times the decks used
Cards in a Shoe lie faced down
The top card of the Shoe is the lastly added Card when no shuffle is done
I can draw a card from the Shoe until it is empty
I can shuffle all cards from the Shoe
[for later extension]
I can shuffle the stack halfway through, but the most recently played Card remains the same in the stack