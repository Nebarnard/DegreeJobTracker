using Microsoft.EntityFrameworkCore;
using DegreeJobTracker.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddDistributedMemoryCache(); // Stores sessions in-memory
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // Security feature to prevent client-side script access to the cookie
    options.Cookie.IsEssential = true; // Mark the session cookie as essential for the application
});

// Add EF Core DI
builder.Services.AddDbContext<DegreeJobTrackerContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DegreeJobTrackerContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.UseSession();

app.Run();
