namespace ProcurementSystem.API.SharedKernel;

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if(Amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative", nameof(amount));
        }
    }



}
