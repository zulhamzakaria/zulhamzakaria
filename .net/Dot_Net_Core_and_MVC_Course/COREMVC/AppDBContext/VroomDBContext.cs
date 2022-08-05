using COREMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace COREMVC.AppDBContext
{
    public class VroomDBContext : DbContext
    {
        public VroomDBContext(DbContextOptions<VroomDBContext> options) : base(options)
        {

        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }

    }
}