using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Rules;

public static class InterviewTaskStatusRules
{
    public static readonly HashSet<InterviewTaskStatus> ActiveStatuses =
      [InterviewTaskStatus.Assigned, InterviewTaskStatus.Accepted];
}


  