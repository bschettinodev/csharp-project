using api.Enums;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(a => a.Name).HasMaxLength(100);
            entity.HasIndex(a => a.Name);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(c => c.Name).HasMaxLength(100);
            entity.Property(c => c.Color).HasMaxLength(7);
            entity.Property(c => c.Icon).HasMaxLength(50);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(t => t.Description).HasMaxLength(150);
            entity.Property(t => t.Notes).HasMaxLength(500);

            entity.HasIndex(t => t.Date);

            entity
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder
            .Entity<Category>()
            .HasData(
                new Category
                {
                    Id = Guid.Parse("11111111-1111-4111-8111-111111111111"),
                    Name = "Salary",
                    Type = TransactionType.Income,
                    Color = "#55E27A",
                    Icon = "salary",
                },
                new Category
                {
                    Id = Guid.Parse("11111111-1111-4111-8111-111111111112"),
                    Name = "Freelance",
                    Type = TransactionType.Income,
                    Color = "#6EE7B7",
                    Icon = "freelance",
                },
                new Category
                {
                    Id = Guid.Parse("11111111-1111-4111-8111-111111111113"),
                    Name = "Investments",
                    Type = TransactionType.Income,
                    Color = "#A3E635",
                    Icon = "investment",
                },
                new Category
                {
                    Id = Guid.Parse("11111111-1111-4111-8111-111111111114"),
                    Name = "Refund",
                    Type = TransactionType.Income,
                    Color = "#22C55E",
                    Icon = "refund",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-222222222221"),
                    Name = "Food",
                    Type = TransactionType.Expense,
                    Color = "#F97316",
                    Icon = "food",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-222222222222"),
                    Name = "Transport",
                    Type = TransactionType.Expense,
                    Color = "#38BDF8",
                    Icon = "transport",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-222222222223"),
                    Name = "Shopping",
                    Type = TransactionType.Expense,
                    Color = "#F472B6",
                    Icon = "shopping",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-222222222224"),
                    Name = "Housing",
                    Type = TransactionType.Expense,
                    Color = "#A78BFA",
                    Icon = "housing",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-222222222225"),
                    Name = "Bills",
                    Type = TransactionType.Expense,
                    Color = "#FBBF24",
                    Icon = "bills",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-222222222226"),
                    Name = "Health",
                    Type = TransactionType.Expense,
                    Color = "#F87171",
                    Icon = "health",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-222222222227"),
                    Name = "Education",
                    Type = TransactionType.Expense,
                    Color = "#60A5FA",
                    Icon = "education",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-222222222228"),
                    Name = "Entertainment",
                    Type = TransactionType.Expense,
                    Color = "#FACC15",
                    Icon = "entertainment",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-222222222229"),
                    Name = "Subscriptions",
                    Type = TransactionType.Expense,
                    Color = "#818CF8",
                    Icon = "subscriptions",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-22222222223a"),
                    Name = "Groceries",
                    Type = TransactionType.Expense,
                    Color = "#34D399",
                    Icon = "groceries",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-22222222223b"),
                    Name = "Travel",
                    Type = TransactionType.Expense,
                    Color = "#2DD4BF",
                    Icon = "travel",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-22222222223c"),
                    Name = "Personal Care",
                    Type = TransactionType.Expense,
                    Color = "#FB7185",
                    Icon = "personal-care",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-22222222223d"),
                    Name = "Pets",
                    Type = TransactionType.Expense,
                    Color = "#C084FC",
                    Icon = "pets",
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-4222-8222-22222222223e"),
                    Name = "Taxes",
                    Type = TransactionType.Expense,
                    Color = "#94A3B8",
                    Icon = "taxes",
                }
            );
    }
}
