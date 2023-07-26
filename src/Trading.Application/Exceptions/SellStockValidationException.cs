using System.ComponentModel.DataAnnotations;

namespace Trading.Application.Exceptions;

[Serializable]
public class SellStockValidationException : ValidationException
{
    public SellStockValidationException(string message) : base(message)
    {

    }
}