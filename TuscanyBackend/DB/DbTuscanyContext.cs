using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TuscanyBackend.DB.Models;

namespace TuscanyBackend.DB;

public partial class DbTuscanyContext : DbContext
{
    public DbTuscanyContext()
    {
    }

    public DbTuscanyContext(DbContextOptions<DbTuscanyContext> options)
        : base(options)
    {
    }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=GEORGE;Initial Catalog=dbTuscany; TrustServerCertificate=True;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gallery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gallery__3214EC07CDEA3ED1");

            entity.ToTable("Gallery");

            entity.Property(e => e.Img).HasMaxLength(2000);

            entity.HasOne(d => d.Tour).WithMany(p => p.Galleries)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gallery__TourId__02084FDA");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Language__3214EC076E0C8B8B");

            entity.Property(e => e.Language1)
                .HasMaxLength(32)
                .HasColumnName("Language");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC0758270108");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReceiverEmail).HasMaxLength(50);
            entity.Property(e => e.ReceiverName).HasMaxLength(32);
            entity.Property(e => e.ReceiverPhoneNumber).HasMaxLength(32);
            entity.Property(e => e.ReceiverSurname).HasMaxLength(32);

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethod)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__PaymentM__17F790F9");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__StatusId__17036CC0");

            entity.HasOne(d => d.TourScheduleNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TourSchedule)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__TourSche__151B244E");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderSta__3214EC07A10D5D69");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.Status).HasMaxLength(32);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3214EC0760D8BC0D");

            entity.Property(e => e.PaymentMethod1)
                .HasMaxLength(32)
                .HasColumnName("PaymentMethod");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tickets__3214EC078D077450");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__OrderId__1EA48E88");

            entity.HasOne(d => d.Type).WithMany(p => p.InverseType)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__TypeId__1F98B2C1");
        });

        modelBuilder.Entity<TicketsType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TicketsT__3214EC07412A0FD1");

            entity.Property(e => e.Type).HasMaxLength(16);
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tours__3214EC07FAD890A8");

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
                .HasConstraintName("FK__Tours__Transport__7B5B524B");
        });

        modelBuilder.Entity<ToursLanguage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ToursLan__3214EC078A48F1CA");

            entity.HasOne(d => d.Language).WithMany(p => p.ToursLanguages)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ToursLang__Langu__07C12930");

            entity.HasOne(d => d.Tour).WithMany(p => p.ToursLanguages)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ToursLang__TourI__08B54D69");
        });

        modelBuilder.Entity<ToursSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ToursSch__3214EC07728BD540");

            entity.ToTable("ToursSchedule");

            entity.Property(e => e.DateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Tour).WithMany(p => p.ToursSchedules)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ToursSche__TourI__04E4BC85");
        });

        modelBuilder.Entity<ToursScheduleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ToursSch__3214EC07D1437B6F");

            entity.ToTable("ToursScheduleType");

            entity.Property(e => e.ScheduleType).HasMaxLength(32);
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transpor__3214EC07769149BC");

            entity.Property(e => e.Name).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
