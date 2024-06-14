using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace P6BusStation_API_CristoferMontero.Models;

public partial class P620242ticketBusContext : DbContext
{
    public P620242ticketBusContext()
    {
    }

    public P620242ticketBusContext(DbContextOptions<P620242ticketBusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Destination> Destinations { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<TicketPurchase> TicketPurchases { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Destination>(entity =>
        {
            entity.HasKey(e => e.DestinationId).HasName("PK__Destinat__DB5FE4AC653DE371");

            entity.ToTable("Destination");

            entity.Property(e => e.DestinationId).HasColumnName("DestinationID");
            entity.Property(e => e.DestinationName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A5837A48B89");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentDate).HasColumnType("smalldatetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TicketPurchaseId).HasColumnName("TicketPurchaseID");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TransactionID");

            entity.HasOne(d => d.TicketPurchase).WithMany(p => p.Payments)
                .HasForeignKey(d => d.TicketPurchaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPayment684162");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PK__Route__80979AAD36180F51");

            entity.ToTable("Route");

            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.FinalDestinationId).HasColumnName("FinalDestinationID");
            entity.Property(e => e.OriginDestinationId).HasColumnName("OriginDestinationID");

            entity.HasOne(d => d.FinalDestination).WithMany(p => p.RouteFinalDestinations)
                .HasForeignKey(d => d.FinalDestinationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKRoute76418");

            entity.HasOne(d => d.OriginDestination).WithMany(p => p.RouteOriginDestinations)
                .HasForeignKey(d => d.OriginDestinationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKRoute749332");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__9C8A5B69F3775C09");

            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.AvailableDate).HasColumnType("smalldatetime");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.WeekDayName)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Route).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKSchedule510219");
        });

        modelBuilder.Entity<TicketPurchase>(entity =>
        {
            entity.HasKey(e => e.TicketPurchaseId).HasName("PK__TicketPu__97683D361F1866D2");

            entity.ToTable("TicketPurchase");

            entity.Property(e => e.TicketPurchaseId).HasColumnName("TicketPurchaseID");
            entity.Property(e => e.PurchaseDate).HasColumnType("smalldatetime");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.Seat)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TicketStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Schedule).WithMany(p => p.TicketPurchases)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTicketPurc555440");

            entity.HasOne(d => d.User).WithMany(p => p.TicketPurchases)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKTicketPurc164624");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACDA03DE10");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Adress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser244251");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A55154CD02E");

            entity.ToTable("UserRole");

            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.UserRoleDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
