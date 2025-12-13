using InterviewSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace InterviewSystem.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Candidate> Candidates => Set<Candidate>();
    public DbSet<InterviewRound> InterviewRounds => Set<InterviewRound>();
    public DbSet<InterviewTask> InterviewTasks => Set<InterviewTask>();
    public DbSet<InterviewProcess> InterviewProcesses => Set<InterviewProcess>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureInterviewRound(modelBuilder);
        ConfigureInterviewTask(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum>()
            .HaveConversion<string>();
        base.ConfigureConventions(configurationBuilder);
    }

    private void ConfigureInterviewTask(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InterviewTask>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Evaluation, eval =>
            {
                eval.Property(ev => ev.Passed)
                .IsRequired()
                .HasColumnName("EvaluationPassed");
                eval.Property(ev => ev.Note);
            });
        });

        modelBuilder.Entity<InterviewTask>()
            .HasOne<Candidate>()
            .WithMany()
            .HasForeignKey(x => x.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<InterviewTask>()
            .HasOne<InterviewRound>()
            .WithMany()
            .HasForeignKey(x => x.InterviewRoundId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureInterviewRound(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InterviewRound>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.OwnsMany(x => x.Items, item =>
            {
                item.WithOwner().HasForeignKey("InterviewRoundId");
                item.Property<int>("Id");
                item.HasKey("Id");

                item.Property(i => i.Sequence).IsRequired();
                item.Property(i => i.Position).IsRequired();

                item.HasIndex("InterviewRoundId", nameof(InterviewRoundItem.Sequence))
                .IsUnique();

                item.ToTable("InterviewRoundItems");

            });
        });
    }
}
