using LifeCounter.Common.Container;
using LifeCounter.Common.Front;
using LifeCounter.DataLayer.Container;
using LifeCounter.DataLayer.Db;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LifeCounterDbContext>();

builder.Services
    .UseUtilities("site")
    .AddDb();
builder.Services.AddDbContext<LifeCounterDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseFront();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();