using System;
using System.Collections.Generic;
using L.I.S.A.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace L.I.S.A.Data
{
    public partial class LISASITEContext : DbContext
    {
        public LISASITEContext()
        {
        }

        public LISASITEContext(DbContextOptions<LISASITEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Case> Cases { get; set; } = null!;
        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<Fleet> Fleets { get; set; } = null!;
        public virtual DbSet<LoadingSite> LoadingSites { get; set; } = null!;
        public virtual DbSet<LoadingSlip> LoadingSlips { get; set; } = null!;
        public virtual DbSet<LogisticsManager> LogisticsManagers { get; set; } = null!;
        public virtual DbSet<OffloadingSite> OffloadingSites { get; set; } = null!;
        public virtual DbSet<OffloadingSlip> OffloadingSlips { get; set; } = null!;
        public virtual DbSet<Permit> Permits { get; set; } = null!;
        public virtual DbSet<Trip> Trips { get; set; } = null!;
        public virtual DbSet<Truck> Trucks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MATHAPELOMMANOI\\MSSQLSERVER2022;Database=LISASITE;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.ContactNum).HasColumnName("contact_num");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.ContactNum)
                    .HasMaxLength(10)
                    .HasColumnName("contact_num");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("last_name");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Case>(entity =>
            {
                entity.HasKey(e => e.CasingId);

                entity.ToTable("cases");

                entity.Property(e => e.CasingId).HasColumnName("casing_id");

                entity.Property(e => e.CasingDesc)
                    .HasMaxLength(50)
                    .HasColumnName("casing_desc");

                entity.Property(e => e.CasingType)
                    .HasMaxLength(50)
                    .HasColumnName("casing_type");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.Resolution)
                    .HasMaxLength(50)
                    .HasColumnName("resolution");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("driver");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.BirthCountry)
                    .HasMaxLength(50)
                    .HasColumnName("birth_country");

                entity.Property(e => e.ContactNum).HasColumnName("contact_num");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.EmpDate)
                    .HasColumnType("date")
                    .HasColumnName("emp_date");

                entity.Property(e => e.EmpNum).HasColumnName("emp_num");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.License).HasColumnName("license");

                entity.Property(e => e.LicenseImg)
                    .HasColumnType("image")
                    .HasColumnName("license_img");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__driver__UserId__2739D489");
            });

            modelBuilder.Entity<Fleet>(entity =>
            {
                entity.ToTable("fleet");

                entity.Property(e => e.FleetId).HasColumnName("fleet_id");

                entity.Property(e => e.Activation)
                    .HasMaxLength(30)
                    .HasColumnName("activation");

                entity.Property(e => e.TruckId).HasColumnName("truck_id");

                entity.HasOne(d => d.Truck)
                    .WithMany(p => p.Fleets)
                    .HasForeignKey(d => d.TruckId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_fleet_trucks");
            });

            modelBuilder.Entity<LoadingSite>(entity =>
            {
                entity.HasKey(e => e.LoadSiteId);

                entity.ToTable("loading_site");

                entity.Property(e => e.LoadSiteId).HasColumnName("load_site_id");

                entity.Property(e => e.LoadSiteName)
                    .HasMaxLength(50)
                    .HasColumnName("load_site_name");
            });

            modelBuilder.Entity<LoadingSlip>(entity =>
            {
                entity.HasKey(e => e.LoadSlipId);

                entity.ToTable("loading_slip");

                entity.Property(e => e.LoadSlipId).HasColumnName("load_slip_id");

                entity.Property(e => e.LoadSlipImg)
                    .HasColumnType("image")
                    .HasColumnName("load_slip_img");
            });

            modelBuilder.Entity<LogisticsManager>(entity =>
            {
                entity.HasKey(e => e.LogisticId);

                entity.ToTable("logistics_manager");

                entity.Property(e => e.LogisticId).HasColumnName("logistic_id");

                entity.Property(e => e.ContactNum).HasColumnName("contact_num");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LogisticsManagers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__logistics__UserI__282DF8C2");
            });

            modelBuilder.Entity<OffloadingSite>(entity =>
            {
                entity.HasKey(e => e.OffloadSiteId);

                entity.ToTable("offloading_site");

                entity.Property(e => e.OffloadSiteId).HasColumnName("offload_site_id");

                entity.Property(e => e.OffloadSiteName)
                    .HasMaxLength(50)
                    .HasColumnName("offload_site_name");
            });

            modelBuilder.Entity<OffloadingSlip>(entity =>
            {
                entity.HasKey(e => e.OffloadSlipId);

                entity.ToTable("offloading_slip");

                entity.Property(e => e.OffloadSlipId).HasColumnName("offload_slip_id");

                entity.Property(e => e.OffloadSlipImg)
                    .HasColumnType("image")
                    .HasColumnName("offload_slip_img");
            });

            modelBuilder.Entity<Permit>(entity =>
            {
                entity.ToTable("Permit");

                entity.Property(e => e.PermitId).HasColumnName("permit_id");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.ExpDate)
                    .HasColumnType("date")
                    .HasColumnName("exp_date");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("date")
                    .HasColumnName("issue_date");

                entity.Property(e => e.PermitImg)
                    .HasColumnType("image")
                    .HasColumnName("permit_img");

                entity.Property(e => e.PermitStatus)
                    .HasMaxLength(50)
                    .HasColumnName("permit_status");

                entity.Property(e => e.PermitType)
                    .HasMaxLength(50)
                    .HasColumnName("permit_type");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Permits)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permit_driver");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.TripsId);

                entity.ToTable("trips");

                entity.Property(e => e.TripsId).HasColumnName("trips_id");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.FleetId).HasColumnName("fleet_id");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.LoadSiteId).HasColumnName("load_site_id");

                entity.Property(e => e.LoadSlipId).HasColumnName("load_slip_id");

                entity.Property(e => e.OffloadSiteId).HasColumnName("offload_site_id");

                entity.Property(e => e.OffloadSlipId).HasColumnName("offload_slip_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.TruckId).HasColumnName("truck_id");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trips_driver");

                entity.HasOne(d => d.Fleet)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.FleetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trips_fleet");

                entity.HasOne(d => d.LoadSite)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.LoadSiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trips_loading_site");

                entity.HasOne(d => d.LoadSlip)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.LoadSlipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trips_loading_slip");

                entity.HasOne(d => d.OffloadSite)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.OffloadSiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trips_offloading_site");

                entity.HasOne(d => d.OffloadSlip)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.OffloadSlipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trips_offloading_slip");
            });

            modelBuilder.Entity<Truck>(entity =>
            {
                entity.ToTable("trucks");

                entity.Property(e => e.TruckId).HasColumnName("truck_id");

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .HasColumnName("company");

                entity.Property(e => e.Make)
                    .HasMaxLength(50)
                    .HasColumnName("make");

                entity.Property(e => e.Trailer1Reg)
                    .HasMaxLength(50)
                    .HasColumnName("trailer1_reg");

                entity.Property(e => e.Trailer2Reg)
                    .HasMaxLength(50)
                    .HasColumnName("trailer2_reg");

                entity.Property(e => e.TruckCondition)
                    .HasMaxLength(50)
                    .HasColumnName("truck_condition");

                entity.Property(e => e.TruckImg)
                    .HasColumnType("image")
                    .HasColumnName("truck_img");

                entity.Property(e => e.TruckStatus)
                    .HasMaxLength(50)
                    .HasColumnName("truck_status");

                entity.Property(e => e.VinNum)
                    .HasMaxLength(50)
                    .HasColumnName("vin_num");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
