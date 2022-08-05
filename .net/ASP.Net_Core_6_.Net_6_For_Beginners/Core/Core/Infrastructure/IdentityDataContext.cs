using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure
{
    /*
        Identity needs a dbcontext class that extends IdentityDbContext
        Register the services inside the program.cs file
        Run migration command line in the project terminal
            - dotnet ef migrations add --context [name_of_context] [name_of_migration]
            - dotnet ef database update --context [name_of_context]
        Artifacts will be creatd and put into Migrations/[name_of_context_w/o_suffix]
        Identity supports roles that are managed by Roles Manager
    */

    public class IdentityDataContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options){}

    }
}
