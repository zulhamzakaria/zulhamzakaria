namespace InterviewSystem.Domain.Common.ErrorHandling.Errors;

public static class GenericErrors
{
    public static Error Required(string fieldName)
        => new($"{fieldName.ToUpper()}_REQUIRED", $"{fieldName} is required");
    public static Error InvalidEnumValue<TEnum>(TEnum value) where TEnum : struct, Enum
        => new($"UNDEFINED_ENUM_VALUE", $"The enum value provided is invalid");

    public static Error InvalidLength(string fieldName, int min, int max)
        => new($"{fieldName.ToUpper()}_LENGTH_INVALID", $"{fieldName} length must be between {min} and {max} characters");
}