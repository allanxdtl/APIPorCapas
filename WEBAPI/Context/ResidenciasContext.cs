using Microsoft.EntityFrameworkCore;
using WEBAPI.Models;

namespace WEBAPI.Context;

public partial class ResidenciasContext : DbContext
{
    public ResidenciasContext() { }

    public ResidenciasContext(DbContextOptions<ResidenciasContext> options)
        : base(options) { }

    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<VentaHeader> Ventas { get; set; }
    public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tabla Roles
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        // Tabla Usuarios
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Nombre).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Apellido).HasMaxLength(100).IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // Tabla Clientes
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Apellido).HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.Telefono).HasMaxLength(20).IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(100).IsUnicode(false);
        });

        // Tabla Productos
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).HasMaxLength(13).IsUnicode(false);
            entity.Property(e => e.Descripcion).HasMaxLength(150).IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10,2)");
        });

        // Tabla Ventas (Header)
        modelBuilder.Entity<VentaHeader>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Folio).HasMaxLength(20).IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(10,2)");
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.Usuario)
                .WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Cliente)
                .WithMany(p => p.Ventas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Tabla Detalles de Venta
        modelBuilder.Entity<VentaDetalle>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10,2)");

            entity.HasOne(d => d.VentaHeader)
                .WithMany(p => p.Detalles)
                .HasForeignKey(d => d.IdVentaHeader)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Producto)
                .WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
