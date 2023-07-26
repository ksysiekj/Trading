namespace Trading.Application.Exceptions;

[Serializable]
public class TradingException : Exception
{
    public TradingException(string message) : base(message)
    {

    }
    public TradingException(string message, Exception inner) : base(message, inner)
    {

    }
}