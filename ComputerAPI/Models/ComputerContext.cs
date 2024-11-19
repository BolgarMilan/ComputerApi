using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ComputerAPI.Models;

public partial class ComputerContext : DbContext
{
    public ComputerContext()
    {
    }

    public ComputerContext(DbContextOptions<ComputerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comp> Comps { get; set; }

    public virtual DbSet<O> Os { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=computer;user=root;password=;sslmode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("comp");

            entity.HasIndex(e => e.OsId, "OsId");

            entity.Property(e => e.Brand)
                .HasMaxLength(37)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Display).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Memory)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.OsId).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Os).WithMany(p => p.Comps)
                .HasForeignKey(d => d.OsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("comp_ibfk_1");
        });

        modelBuilder.Entity<O>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("os");

            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(27)
                .HasDefaultValueSql("'NULL'");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
