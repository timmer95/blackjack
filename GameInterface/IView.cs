using System;
using System.Collections.Generic;
using System.Text;

namespace GameInterface;

public interface IView
{
    public void DisplayMessage(string message, bool endOnSameLine = false);
    public string ReadInput();

}
