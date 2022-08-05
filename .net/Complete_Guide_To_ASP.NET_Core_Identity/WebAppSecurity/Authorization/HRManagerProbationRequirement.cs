using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace WebAppSecurity.Authorization
{
    public class HRManagerProbationRequirement : IAuthorizationRequirement
    {
        public int probationMonths;
        public HRManagerProbationRequirement(int probationMonths)
        {
            this.probationMonths = probationMonths;
        }
    }

    public class HRManagerProbationRequirementHandler : AuthorizationHandler<HRManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Type == "EmployeeDate"))
                return Task.CompletedTask;

            var empDate = DateTime.Parse(context.User.FindFirst(x => x.Type == "EmploymentDate").Value);
            var period = DateTime.Now - empDate;
            if(period.Days > 30 * requirement.probationMonths) context.Succeed(requirement);

            return Task.CompletedTask;

        }
    }
}
