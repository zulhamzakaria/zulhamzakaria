namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public static class GenericErrors
{
    public static Error Required(string fieldName)
        => new($"{fieldName.ToUpper()}_REQUIRED", $"{Humanize(fieldName)} is required");
    public static Error InvalidEnumValue<TEnum>(TEnum value) where TEnum : struct, Enum
        => new($"UNDEFINED_{typeof(TEnum).Name.ToUpper()}_VALUE", $"The value provided for {typeof(TEnum).Name} is invalid");
    public static Error InvalidLength(string fieldName, int min, int max)
        => new($"{fieldName.ToUpper()}_LENGTH_INVALID", $"{Humanize(fieldName)} length must be between {min} and {max} characters");
    public static Error InvalidIntValue(string fieldName)
        => new($"{fieldName.ToUpper()}_VALUE_INVALID", $"{Humanize(fieldName)} must be bigger than 0");
    public static Error NotFound(string fieldName, Guid id)
        => new($"{fieldName.ToUpper()}_NOT_FOUND", $"No {fieldName} found for id: {id}");

    private static string Humanize(string input)
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