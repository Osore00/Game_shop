using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication5.Data;
using WebApplication5.Models;
using WebApplication5.Service;
using Microsoft.AspNetCore.Identity.UI;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connection = "Server = (localdb)\\mssqllocaldb; Database = games; Trusted_Connection = True;" ;
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connection)); 
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "StreifAuth";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDistributedMemoryCache();


//����������� �������� ����������� ��� Admin area
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminArea", policy => { policy.RequireRole("Admin"); });
});



builder.Services.AddMvc(x =>
{
    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
}).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();


builder.Services.AddLocalization(o => { o.ResourcesPath = "Resources"; });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
                    new CultureInfo("uk-UA"),
                    new CultureInfo("en")
                };

    options.DefaultRequestCulture = new RequestCulture("uk-UA");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});



builder.Services.AddIdentity<User, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppDBContext>();


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

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy();
app.UseSession();
app.UseRequestLocalization();

app.MapControllerRoute(name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default2",pattern: "{controller=Game}/{action=Game}/{id?}");
app.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();



