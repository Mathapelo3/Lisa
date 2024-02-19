using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using L.I.S.A.Areas.Identity.Data;
using L.I.S.A.Data;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DBContextConnection") ?? throw new InvalidOperationException("Connection string 'DBContextConnection' not found.");

builder.Services.AddDbContext<LISASITEContext>(options =>
          options.UseSqlServer(connectionString));

builder.Services.AddTransient<IDbConnection>(options =>
        new SqlConnection(builder.Configuration.GetConnectionString("DBContextConnection")));

builder.Services.AddDbContext<L.I.S.A.DBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<LISAUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<L.I.S.A.DBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Logistics Manager", "Driver", "Mechanic" };

    foreach (var role in roles)
    {

        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

}

app.Run();
