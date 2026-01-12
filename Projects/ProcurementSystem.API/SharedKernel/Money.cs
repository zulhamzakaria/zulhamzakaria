using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

namespace ProcurementSystem.API.SharedKernel;

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Result<Unit> Money(decimal amount, string currency)
    {
        if(Amount < 0)
            return Result<Unit>.Failure(CommonErrors.NegativeAmount());

    }



}
