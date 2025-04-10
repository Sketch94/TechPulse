using Microsoft.EntityFrameworkCore;
using TechPulse.Data;

var builder = WebApplication.CreateBuilder(args);

// L�gg till tj�nster f�r Entity Framework och DbContext
builder.Services.AddDbContext<TechPulseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Konfigurera HTTP-beg�ringspipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseSession();
app.Run();
