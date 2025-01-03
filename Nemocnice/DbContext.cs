using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nemocnice.Models;
using System.Diagnostics;

public class DbContext : IdentityDbContext<AppUser>
{

    public DbContext(DbContextOptions<DbContext> options) : base(options) { }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Pacients { get; set; }
    public DbSet<Hospitalization> Hospitalizations { get; set; }
}