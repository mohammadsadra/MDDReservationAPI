using MDDReservationAPI.Enums;
using MDDReservationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MDDReservationAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>()
            .HasData(
                new Admin()
                {
                    Id = 1,
                    Name = "MohammadSadra Haeri",
                    Role = (int) RoleEnum.Admin,
                    Email = "mohammadsadrahaeri@gmail.com",
                    Phone = "09127959211",
                    CreatedAt = DateTime.UtcNow,
                    IsVerify = true
                }
            );
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<SchoolClass> SchoolsClasses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<RegistrationForm> RegistrationForms { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<EventDays> EventDays { get; set; }
    
    public DbSet<FileDetails> FileDetails { get; set; }
}