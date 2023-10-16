using BusinessLayer.Container;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using TraversalCoreProje.CQRS.Commands.DestinationCommands;
using TraversalCoreProje.CQRS.Handlers.DestinationHandlers;
using TraversalCoreProje.Models;

var builder = WebApplication.CreateBuilder(args);

//Loglama Configure
builder.Services.AddLogging(x=>
{
    x.ClearProviders(); //Clear
    x.SetMinimumLevel(LogLevel.Debug); //start
    x.AddDebug(); //to where

});

builder.Services.AddScoped<GetAllDestinationQueryHandler>(); //CQRS Configure
builder.Services.AddScoped<GetDestinationByIdQueryHandler>(); //CQRS Configure
builder.Services.AddScoped<CreateDestinationCommandHandler>(); //CQRS Configure
builder.Services.AddScoped<RemoveDestinationCommandHandler>(); //CQRS Configure
builder.Services.AddScoped<UpdateDestinationCommandHandler>(); //CQRS Configure

builder.Services.AddMediatR(typeof(Program)); //MediatR Configure


// Add services to the container.
//Identity configure yapýlanmasýný gerçekleþtirdik.
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<Context>(); //Identity validator configure.

builder.Services.AddHttpClient(); //gelen requestleri karþýlayacaðýz.

builder.Services.ContainerDependencies(); //Dependency Configure
builder.Services.AddAutoMapper(typeof(Program)); //Automapper Configure

builder.Services.CustomerValidator();




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
