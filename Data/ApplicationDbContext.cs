using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppCapacitacion.Models;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Instructor> Instructores { get; set; }
    public DbSet<Capacitacion> Capacitaciones { get; set; }
    public DbSet<Evaluacion> Evaluaciones { get; set; }
    public DbSet<EmpleadoCapacitacion> EmpleadoCapacitaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EmpleadoCapacitacion>()
            .HasKey(ec => new { ec.EmpleadoId, ec.CapacitacionId });

        modelBuilder.Entity<Evaluacion>()
            .Property(e => e.Calificacion)
            .HasPrecision(4, 2);
    }
}
