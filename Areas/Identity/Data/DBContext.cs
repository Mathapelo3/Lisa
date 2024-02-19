using L.I.S.A.Areas.Identity.Data;
using L.I.S.A.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L.I.S.A;

public class DBContext : IdentityDbContext<LISAUser>
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DriverVM> Drivers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<LISAUser>
{
    public void Configure(EntityTypeBuilder<LISAUser> builder)
    {
        builder.Property(x => x.first_name).HasMaxLength(100);
        builder.Property(x => x.last_name).HasMaxLength(100);
        builder.Property(x => x.contact_num).HasMaxLength(10);
    }
}