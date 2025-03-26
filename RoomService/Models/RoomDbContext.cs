using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RoomService.Models;

public partial class RoomDbContext : DbContext
{
    public RoomDbContext()
    {
    }

    public RoomDbContext(DbContextOptions<RoomDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rate> Rates { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=LIN-5CG4464LZR\\SQLEXPRESS;Initial Catalog=HotelManagementSystemDb;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasIndex(e => e.RoomId, "IX_Rates_RoomId");

            entity.Property(e => e.ExtensionPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FirstNightPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Room).WithMany(p => p.Rates).HasForeignKey(d => d.RoomId);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasIndex(e => e.GuestId, "IX_Rooms_GuestId");

            entity.Property(e => e.RoomID).HasColumnName("RoomID");
            entity.Property(e => e.Period).HasMaxLength(20);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RoomType).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
