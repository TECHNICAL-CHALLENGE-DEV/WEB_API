using System;
using System.Collections.Generic;
using Cirkula.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkula.API.Data;

public partial class CirkulaContext : DbContext
{
    public CirkulaContext()
    {
    }

    public CirkulaContext(DbContextOptions<CirkulaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Store>(entity =>
        {
            entity.ToTable("Store");

            entity.Property(e => e.BannerUrl)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CloseTime).HasPrecision(3);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OpenTime).HasPrecision(3);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
