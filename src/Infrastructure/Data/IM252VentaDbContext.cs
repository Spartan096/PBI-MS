using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure
{
    public class IM252VentaDbContext : DbContext
    {
        private readonly string _connectionString;

        public IM252VentaDbContext(DbContextOptions<IM252VentaDbContext> options)
            : base(options)
        {
            _connectionString = options.FindExtension<Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal.SqlServerOptionsExtension>()?.ConnectionString 
                                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public DbSet<IM252Venta> IM252Venta { get; set; }
        public DbSet<IM252Cliente> IM252Cliente { get; set; }
        public DbSet<IM252Producto> IM252Producto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IM252Venta>()
                .HasKey(v => v.Id); // Primary Key

            modelBuilder.Entity<IM252Venta>()
                .HasOne(v => v.Cliente)
                .WithMany() 
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IM252Venta>()
                .HasOne(v => v.Producto)
                .WithMany() 
                .HasForeignKey(v => v.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
