using InterviewSystem.Domain.Common.Enums;
using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;

namespace InterviewSystem.Domain.Entity;

public class Candidate : EntityBase
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public AppliedPosition AppliedPosition { get; private set; }

    private Candidate()
    {
        //EF Core needs this
    }

    private Candidate(Guid id, string name, string email, string phoneNumber, AppliedPosition appliedPosition)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        AppliedPosition = appliedPosition;
    }

    public static Result<Candidate> Create(string name, string email, string phoneNumber, AppliedPosition appliedPosition)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Candidate>.Failure(GenericErrors.Required(nameof(name)));

        if (string.IsNullOrWhiteSpace(email))
            return Result<Candidate>.Failure(GenericErrors.Required(nameof(email)));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result<Candidate>.Failure(GenericErrors.Required(nameof(phoneNumber)));

        if (Enum.IsDefined(typeof(AppliedPosition), appliedPosition) is false)
            return Result<Candidate>.Failure(GenericErrors.InvalidEnumValue(appliedPosition));

        var candidate = new Candidate(
            Guid.NewGuid(),
            name,
            email,
            phoneNumber,
            appliedPosition
            );

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
