namespace GameInterface;

public class View : IView
{
    public delegate void ConsoleWriter(string message);
    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void DisplayMessage(string message, bool endOnSameLine = false)
    {
        ConsoleWriter func = endOnSameLine ? Console.Write : Console.WriteLine;
        func(message);
    }

    public string ReadInput()
    {
        return Console.ReadLine() ?? "";
    }
}
