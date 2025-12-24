using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Common.ErrorHandling.Errors;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;
using Unit = InterviewSystem.Domain.Common.Unit;

namespace InterviewSystem.Application.Employees.DeactivateEmployee;

public sealed class DeactivateEmployeeHandler : IRequestHandler<DeactivateEmployeeCommand, Result<Unit>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IInterviewTaskRepository _interviewTaskRepository;
    private readonly IUnitOfWorkRepository _uow;

    public DeactivateEmployeeHandler(IEmployeeRepository employeeRepository, IUnitOfWorkRepository uow, 
        IInterviewTaskRepository interviewTaskRepository)
    {
        _employeeRepository = employeeRepository;
        _uow = uow;
        _interviewTaskRepository = interviewTaskRepository;
    }

    public async Task<Result<Unit>> Handle(DeactivateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.employeeId);
        if (employee is null)
            return Result<Unit>.Failure(GenericErrors.NoRecordFound(nameof(Employee), request.employeeId));

        //Pending task
        var tasks = await _interviewTaskRepository.GetAllByEmployeeId(request.employeeId);
        if (tasks.Any())
            return Result<Unit>.Failure(InterviewTaskErrors.PendingTasks());

        employee.Deactivate();
        await _uow.SaveChangesAsync();
        return Result<Unit>.Success(Unit.Value);
    }

}
