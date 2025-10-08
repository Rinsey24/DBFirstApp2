using Microsoft.EntityFrameworkCore;
using DBFirstApp.Models;
namespace DBFirstApp.Data;

public class StudentContext : DbContext
{
    public DbSet<Student> Students { get; set; } = null!;

    public StudentContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=students.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка таблицы согласно требованиям
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Students");
            entity.HasKey(e => e.StudentId);
                
            entity.Property(e => e.StudentId)
                .HasColumnName("student_id")
                .ValueGeneratedOnAdd();
                
            entity.Property(e => e.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(50)
                .IsRequired();
                
            entity.Property(e => e.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(50)
                .IsRequired();
                
            entity.Property(e => e.DateOfBirth)
                .HasColumnName("date_of_birth")
                .IsRequired();
                
            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(100);

            // Уникальный индекс для email
            entity.HasIndex(e => e.Email)
                .IsUnique();
        });
    }
}
