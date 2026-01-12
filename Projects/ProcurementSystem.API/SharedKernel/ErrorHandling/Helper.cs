namespace ProcurementSystem.API.SharedKernel.ErrorHandling;

public static class Helper
{
    public static string Humanize(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        var result = System.Text.RegularExpressions.Regex.Replace(
            input,
            "(\\B[A-Z])",
            " $1"
        );

        return char.ToUpper(result[0]) + result.Substring(1);
    }
}
