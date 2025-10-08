using Microsoft.EntityFrameworkCore;
using DBFirstApp.Models;

namespace DBFirstApp.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Groups");
                entity.HasKey(e => e.GroupId);
                
                entity.Property(e => e.GroupName)
                    .HasColumnName("group_name")
                    .HasMaxLength(50)
                    .IsRequired();
                    
                entity.Property(e => e.Course)
                    .HasColumnName("course")
                    .HasMaxLength(50)
                    .IsRequired();
            });

           
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100);

                
                entity.HasOne(u => u.Group)          
                    .WithMany(g => g.Users)         
                    .HasForeignKey("GroupId")         
                    .OnDelete(DeleteBehavior.Cascade); 
            });
        }
    }
}