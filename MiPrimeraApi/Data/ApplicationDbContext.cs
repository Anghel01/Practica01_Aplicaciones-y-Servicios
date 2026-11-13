using Microsoft.EntityFrameworkCore;
using MiPrimeraApi.Models;

namespace MiPrimeraApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de Usuarios
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.HasKey(usuario => usuario.IdUsuario);
            entity.Property(usuario => usuario.IdUsuario)
                .ValueGeneratedOnAdd();
            entity.Property(usuario => usuario.NombreUsuario)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(usuario => usuario.CorreoElectronicousuario)
                .HasMaxLength(150)
                .IsRequired();
            entity.Property(usuario => usuario.Contrasenausuario)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(usuario => usuario.Activo)
                .IsRequired();
        });

        // Configuración de Producto
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.Property(p => p.Nombre).HasMaxLength(100);
            entity.Property(p => p.Precio).HasPrecision(18, 2);
        });

        // Configuración de Proveedor (ITID803_02)
        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.Property(p => p.Nombre).HasMaxLength(100);
            entity.Property(p => p.Telefono).HasMaxLength(20);
        });
    }
}
