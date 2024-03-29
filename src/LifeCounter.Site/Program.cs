using LifeCounter.Common.Container;
using LifeCounter.Common.Front;
using LifeCounter.Common.Utilities;
using LifeCounter.DataLayer.Container;
using LifeCounter.DataLayer.Db;
using LifeCounter.Site.Extensions;
using LifeCounter.Site.Models;
using LifeCounter.Site.Models.Email;
using LifeCounter.Site.Models.TokenProvider;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.secret.json");

builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<LifeCounterDbContext>()
    .AddTokenProvider<SingleValidateTokenProvider<IdentityUser>>(Constants.AUTH_TOKEN_PROVIDER)
    ;

builder.Services
    .Configure<DataProtectionTokenProviderOptions>(e => e.TokenLifespan = builder.Configuration.GetTokenTtl());

builder.Services
    .AddHttpClient()
    .UseAutoDependencies(typeof(Program).Assembly)
    .UseUtilities("site")
    .UseDb()
    .UseTempStorage()
    .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
    .AddTransient<IViewRenderService, ViewRenderService>()
    .AddTransient<IEmailSender, MailgunEmailSender>()
    ;

builder.Services.AddMvc();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages(e =>
{
    e.Conventions.AuthorizeAreaFolder("Admin", "/");
});

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

app.UseFront();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();