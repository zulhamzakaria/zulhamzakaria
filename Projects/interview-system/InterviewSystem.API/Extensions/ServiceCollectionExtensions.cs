using InterviewSystem.Application.Candidates.CreateCandidate;

namespace InterviewSystem.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //Candidate
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCandidateCommand).Assembly));

        return services;
    }
}
