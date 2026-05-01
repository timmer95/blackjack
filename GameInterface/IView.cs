using Delegates;

namespace GameInterface;

public interface IView
{
    public void DisplayMessage(string message, bool endOnSameLine = false);
    public string ReadInput();

    public T GetValidatedInput<T>(string message, Validator<T> validatorF)
    {
        T? result = default;
        bool keepAsking = true;
        while (keepAsking || result is null)
        {
            this.DisplayMessage(message);
            string inputS = this.ReadInput();
            keepAsking = !validatorF(inputS, out result);
        }
        return result!;
    }
}
