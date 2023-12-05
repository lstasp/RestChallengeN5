using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RestChallengeN5.Models;

public partial class DbChallengeN5Context : DbContext
{
    public DbChallengeN5Context()
    {
    }

    public DbChallengeN5Context(DbContextOptions<DbChallengeN5Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PermissionType> PermissionTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=localhost; database=dbChallengeN5;Trusted_Connection=SSPI;MultipleActiveResultSets=true;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.Property(e => e.EmployeeForename).HasColumnType("text");
            entity.Property(e => e.EmployeeSurname).HasColumnType("text");
            entity.Property(e => e.PermissionDate).HasColumnType("date");

            entity.HasOne(d => d.PermissionTypeNavigation).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PermissionType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permissions_PermissionTypes");
        });

        modelBuilder.Entity<PermissionType>(entity =>
        {
            entity.Property(e => e.Description).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
