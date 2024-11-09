using System;
using System.Collections.Generic;
using Assignment.DBO.Tables;
using Microsoft.EntityFrameworkCore;

namespace Assignment.DBO;

public partial class DbAa587cAssesmentContext : DbContext
{

    private readonly IConfiguration _configuration;
    public DbAa587cAssesmentContext()
    {
    }

    public DbAa587cAssesmentContext(DbContextOptions<DbAa587cAssesmentContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasIndex(e => e.AccountNumber, "IX_Accounts").IsUnique();

            entity.Property(e => e.AccountHolderName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FromAccount)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.ToAccount)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionDescription)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
