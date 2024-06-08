using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tuscany.Models;
using Tuscany.Utility;

namespace Tuscany.DataAccess.DB;

public partial class DbTuscanyContext : IdentityDbContext<User>
{
    public DbTuscanyContext() { }

    public DbTuscanyContext(DbContextOptions<DbTuscanyContext> options)
        : base(options) { }


    public virtual DbSet<Gallery> Galleries { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketsType> TicketsTypes { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<ToursLanguage> ToursLanguages { get; set; }

    public virtual DbSet<ToursSchedule> ToursSchedules { get; set; }

    public virtual DbSet<ToursScheduleType> ToursScheduleTypes { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC0761075742");

            entity.ToTable("Comment");

            entity.Property(e => e.Text).HasMaxLength(1000);

            entity.HasOne(d => d.Tour).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__TourId__42CFD17E");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__UserId__8E2F1I7O");
        });

        modelBuilder.Entity<Gallery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gallery__3214EC0761075742");

            entity.ToTable("Gallery");

            entity.Property(e => e.Img).HasMaxLength(2000);

            entity.HasOne(d => d.Tour).WithMany(p => p.Galleries)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gallery__TourId__48CFD27E");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Language__3214EC079A30EB58");

            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("Language");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC078848EF55");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__PaymentM__5EBF139D");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__StatusId__5DCAEF64");

            entity.HasOne(d => d.TourScheduleNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TourSchedule)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__TourSche__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Orders_To_User");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderSta__3214EC072A1429BD");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.Status).HasMaxLength(32);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3214EC07477C3DEC");

            entity.Property(e => e.Method)
                .HasMaxLength(32)
                .HasColumnName("PaymentMethod");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tickets__3214EC07C5423841");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__OrderId__656C112C");

            entity.HasOne(d => d.Type).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_To_TicketType");
        });

        modelBuilder.Entity<TicketsType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TicketsT__3214EC07E2A2C659");

            entity.Property(e => e.Type).HasMaxLength(16);
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tours__3214EC07E8E8015F");

            entity.Property(e => e.AdultPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChildPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DepAndArrivAreas).HasMaxLength(32);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Details).HasMaxLength(2000);
            entity.Property(e => e.EntryFees).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Name).HasMaxLength(32);

            entity.HasOne(d => d.Transport).WithMany(p => p.Tours)
                .HasForeignKey(d => d.TransportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tours__Transport__4222D4EF");
        });

        modelBuilder.Entity<ToursLanguage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ToursLan__3214EC07DECEEAC7");

            entity.HasOne(d => d.Language).WithMany(p => p.ToursLanguages)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ToursLang__Langu__4E88ABD4");

            entity.HasOne(d => d.Tour).WithMany(p => p.ToursLanguages)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ToursLang__TourI__4F7CD00D");
        });

        modelBuilder.Entity<ToursSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ToursSch__3214EC07114C5514");

            entity.ToTable("ToursSchedule");

            entity.Property(e => e.DateTime).HasColumnType("datetime");

            entity.HasOne(d => d.ScheduleType).WithMany(p => p.ToursSchedules)
                .HasForeignKey(d => d.ScheduleTypeId)
                .HasConstraintName("FK_Schedule_To_ScheduleType");

            entity.HasOne(d => d.Tour).WithMany(p => p.ToursSchedules)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ToursSche__TourI__4BAC3F29");
        });

        modelBuilder.Entity<ToursScheduleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ToursSch__3214EC07A67A3A2B");

            entity.ToTable("ToursScheduleType");

            entity.Property(e => e.ScheduleType).HasMaxLength(32);
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transpor__3214EC0763F393C4");

            entity.Property(e => e.Name).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
