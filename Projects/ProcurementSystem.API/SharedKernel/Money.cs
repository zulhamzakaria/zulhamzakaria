using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

namespace ProcurementSystem.API.SharedKernel;

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Result<Money> Create(decimal amount, string currency)
    {
        if (amount < 0)
            return Result<Money>.Failure(CommonErrors.NegativeAmount());
        if (string.IsNullOrWhiteSpace(currency))
            return Result<Money>.Failure(CommonErrors.Required(nameof(currency)));

        var money = new Money(amount, currency.ToUpperInvariant());
        return Result<Money>.Success(money);
    }

    public Result<Money> Add(Money other)
    {
        if(Currency != other.Currency)
            return Result<Money>.Failure(CommonErrors.InvalidInput("Currency mismatch"));

        var money = new Money(Amount + other.Amount, Currency);
        return Result<Money>.Success(money);
    }

    public Result<Money> Subtract(Money other)
    {
        if(Currency != other.Currency)
            return Result<Money>.Failure(CommonErrors.InvalidInput("Currency mismatch"));
        if(Amount - other.Amount < 0)
            return Result<Money>.Failure(CommonErrors.NegativeAmount());

        var money = new Money(Amount - other.Amount, Currency);
        return Result<Money>.Success(money);
    }

    public Result<Money> Multiply(decimal factor)
    {
        if(factor < 0)
            return Result<Money>.Failure(CommonErrors.InvalidInput("Negative multiplication factor"));

        var money = new Money(Amount * factor, Currency);
        return Result<Money>.Success(money);
    }

}
