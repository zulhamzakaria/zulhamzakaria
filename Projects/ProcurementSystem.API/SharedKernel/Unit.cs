namespace ProcurementSystem.API.SharedKernel;

public sealed class Unit
{
    public static readonly Unit Value = new Unit();
    private Unit() { }
}
