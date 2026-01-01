using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Rules;

public static class InterviewTaskRules
{
    public static readonly HashSet<InterviewTaskStatus> ActiveStatuses =
      [InterviewTaskStatus.Assigned, InterviewTaskStatus.Accepted];
}


  