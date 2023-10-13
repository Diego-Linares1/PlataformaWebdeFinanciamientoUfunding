using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PWFU.Models;

namespace PWFU.DAL;

public class ApplicationDbContext: IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<Reward> Rewards { get; set; }
    public DbSet<StudentInfo> StudentInfos { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Role>().ToTable("roles");
        modelBuilder.Entity<UserRole>().ToTable("user_roles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");


        modelBuilder.Entity<User>(b =>
        {
            b.Property(u => u.Id).HasDefaultValueSql("gen_random_uuid()");

            b.HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

            b.HasMany(e => e.Projects)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId)
                .IsRequired();

            b.HasMany(e => e.Donations)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            // Each User can have many UserLogins
            b.HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(ul => ul.UserId)
                .IsRequired();

            // Each User can have many UserTokens
            b.HasMany(e => e.Tokens)
                .WithOne()
                .HasForeignKey(ut => ut.UserId)
                .IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<Role>(b =>
        {
            b.Property(u => u.Id).HasDefaultValueSql("gen_random_uuid()");

            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });

        modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<Category>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<Project>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<Reward>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<StudentInfo>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<Donation>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
    }
}