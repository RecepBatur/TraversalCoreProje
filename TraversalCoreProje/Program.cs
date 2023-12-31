using BusinessLayer.Container;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using System.Globalization;
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
//Identity configure yap�lanmas�n� ger�ekle�tirdik.
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomIdentityValidator>().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider).AddEntityFrameworkStores<Context>(); //Identity validator configure.

builder.Services.AddHttpClient(); //gelen requestleri kar��layaca��z.

builder.Services.ContainerDependencies(); //Dependency Configure
builder.Services.AddAutoMapper(typeof(Program)); //Automapper Configure

builder.Services.CustomerValidator();




builder.Services.AddControllersWithViews().AddFluentValidation();

//Proje seviyesinde Authorize konfig�rasyonu yapt�k.
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser() //kullan�c� mutlaka authenticate olsun.
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources"; //dil dosyalar�n� nerede hangi klas�rde arayacak onu belirledik.
});

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    options.SetDefaultCulture("tr");
//    options.AddSupportedUICultures("en", "fr", "es", "gr", "tr", "de");
//    options.FallBackToParentUICultures = true;
//    options.RequestCultureProviders.Clear();
//});


builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/SignIn/"; //E�er bir kullan�c� Authentication olmadan giri� yapm��sa buraya y�nlendir.
});

var app = builder.Build();

//var path = Directory.GetCurrentDirectory();

//Log.Logger = new LoggerConfiguration()
//    .Enrich.FromLogContext()
//    .WriteTo.File($"{path}\\Logs\\Log1.txt", restrictedToMinimumLevel: LogEventLevel.Debug) // Loglar� belirtilen dosyaya yaz
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

//var suppertedCultures = new[] { "en", "fr", "es", "gr", "tr", "de" }; //desteklenen dilleri yazd�k.
//var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(suppertedCultures[1]).AddSupportedCultures(suppertedCultures).AddSupportedCultures(suppertedCultures); //SetDefaultCulture metodu uygulamada ilgili sayfa aya�a kalkt��� zaman default olarak hangi dil ile aya�a kalkt���n� belirtir. Biz [1] yani en olarak belirledik. Daha sonras�nda AddSupportedCultures metodu ile ilk backend taraf�na ekledik sonrada frontend yani UI taraf�na ekledik.
var supportedCultures = new List<CultureInfo>
{
    new CultureInfo("en"),
    new CultureInfo("fr"),
    new CultureInfo("es"),
    new CultureInfo("gr"),
    new CultureInfo("tr"),
    new CultureInfo("de")
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("tr"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    FallBackToParentUICultures = true
};


app.UseRequestLocalization(localizationOptions);

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
