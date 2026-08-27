using System;
using System.Collections.Generic;
using EmployeeDataExporter.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDataExporter.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("Employees_pkey");

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Contact).HasMaxLength(20);
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
