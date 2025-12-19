namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public static class Helper
{
    public static string Humanize(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        // split camelCase / PascalCase into words
        // get the first capital case in the middle of the fieldname i.e I in candidateId 
        var result = System.Text.RegularExpressions.Regex.Replace(
            input,
            "(\\B[A-Z])",
            " $1"
        );

        // capitalize first letter
        return char.ToUpper(result[0]) + result.Substring(1);
    }
}
