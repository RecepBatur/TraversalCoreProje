using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Container;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DTOLayer.DTOs.AnnouncementDTOs;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using Serilog.Events;
using System.IO;
using System.Xml.Linq;
using TraversalCoreProje.Models;

var builder = WebApplication.CreateBuilder(args);

//Loglama Configure
builder.Services.AddLogging(x=>
{
    x.ClearProviders(); //Clear
    x.SetMinimumLevel(LogLevel.Debug); //start
    x.AddDebug(); //to where

});


// Add services to the container.
//Identity configure yapýlanmasýný gerçekleþtirdik.
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<Context>(); //Identity validator configure.

builder.Services.ContainerDependencies(); //Dependency Configure
builder.Services.AddAutoMapper(typeof(Program)); //Automapper Configure

builder.Services.AddTransient<IValidator<AnnouncementAddDto>, AnnouncementValidator>();


builder.Services.AddControllersWithViews().AddFluentValidation();

//Proje seviyesinde Authorize konfigürasyonu yaptýk.
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser() //kullanýcý mutlaka authenticate olsun.
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMvc();


var app = builder.Build();

//var path = Directory.GetCurrentDirectory();

//Log.Logger = new LoggerConfiguration()
//    .Enrich.FromLogContext()
//    .WriteTo.File($"{path}\\Logs\\Log1.txt", restrictedToMinimumLevel: LogEventLevel.Debug) // Loglarý belirtilen dosyaya yaz
//    .CreateLogger();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
