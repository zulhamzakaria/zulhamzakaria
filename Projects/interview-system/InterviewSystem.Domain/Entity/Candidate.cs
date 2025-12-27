using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Entity;

public class Candidate : EntityBase
{
    private const int MinLength = 1;
    private const int MaxNameLength = 100;
    private const int MaxEmailLength = 100;
    private const int MaxPhoneNumberLength = 20;

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public AppliedPosition AppliedPosition { get; private set; }

    private Candidate()
    {
        //EF Core needs this
    }
    public static Result<Candidate> Create(string name, string email, string phoneNumber, AppliedPosition appliedPosition)
    {

        List<Error> errors = new ();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(GenericErrors.Required(nameof(name)));

        if (string.IsNullOrWhiteSpace(email))
            errors.Add(GenericErrors.Required(nameof(email)));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            errors.Add(GenericErrors.Required(nameof(phoneNumber)));

        if(name.Length > MaxNameLength)
            errors.Add(GenericErrors.InvalidLength(nameof(name), MinLength, MaxNameLength));

        if(email.Length > MaxEmailLength)
            errors.Add(GenericErrors.InvalidLength(nameof(name), MinLength, MaxEmailLength));

        if(phoneNumber.Length > MaxPhoneNumberLength)
            errors.Add(GenericErrors.InvalidLength(nameof(name), MinLength, MaxPhoneNumberLength));

        if (Enum.IsDefined(typeof(AppliedPosition), appliedPosition) is false)
            errors.Add(GenericErrors.InvalidEnumValue(appliedPosition));

        if(errors.Any())
            return Result<Candidate>.Failure(errors);

        var candidate = new Candidate()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber,
            AppliedPosition = appliedPosition
        };

        return Result<Candidate>.Success(candidate);
    }

    public void UpdateEmail(string email)
    {
        //TODO: use Result<T>
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new Exception("where email");
        }
        Email = email;
        SetUpdated();
    }

    public void UpdatePhoneNumber(string phoneNumber)
    {
        //TODO: use Result<T>
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new Exception("where email");
        }
        PhoneNumber = phoneNumber;
        SetUpdated();
    }
}
