using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ExpenseTrackerAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ===================== Users =====================
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(u => u.Id);

            entity.Property(u => u.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(256);

            entity.HasIndex(u => u.Email)
                  .IsUnique();

            entity.Property(u => u.PasswordHash)
                  .IsRequired()
                  .HasMaxLength(256);

            entity.Property(u => u.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()");
        });

        // ===================== Categories =====================
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasIndex(c => c.Name)
                  .IsUnique();

            entity.Property(c => c.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()");
        });

        // ===================== Expenses =====================
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.ToTable("Expenses");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Amount)
                  .IsRequired()
                  .HasColumnType("decimal(18,2)");

            entity.ToTable(t => t.HasCheckConstraint("CK_Expenses_Amount", "[Amount] >= 0"));

            entity.Property(e => e.Description)
                  .HasMaxLength(500);

            entity.Property(e => e.ExpenseDate)
                  .IsRequired();

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()");

            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.CategoryId);
            entity.HasIndex(e => e.ExpenseDate);

            // Expense -> User (cascade delete)
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Expenses)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Expense -> Category (no cascade, matches SQL script)
            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Expenses)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.NoAction);
        });
    }
}