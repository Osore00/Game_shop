using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication5.Data;
using WebApplication5.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
string connection = "Server = (localdb)\\mssqllocaldb; Database = games; Trusted_Connection = True;" ;
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connection));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default2",
    pattern: "{controller=Game}/{action=Game}/{id?}");
app.Run();
