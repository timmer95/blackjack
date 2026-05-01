using BlackJackClasses;
using GameInterface;
using System.ComponentModel.Design;

class Program
{
    static void Main()
    {
        View view = new View();
        Game game = new Game(view);

        game.SetUp();
        game.Play();
    }
}