using Microsoft.EntityFrameworkCore;
using SignalRApiForSql.DAL;
using SignalRApiForSql.Hubs;
using SignalRApiForSql.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration["DefaultConnection"]);
});

builder.Services.AddScoped<VisitorService>();
builder.Services.AddSignalR();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        //d��ar�dan herhangi bir kayna��n benim serverimi yani api projemi consume etmesine yani t�ketmesine olanak sa�layan k�s�m buras�.
        builder.AllowAnyHeader() //d��ar�dan herhangi bir ba�l���n gelmesine izin ver.
        .AllowAnyMethod() //d��ar�dan herhangi bir metodun gelmesine izin ver.
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();
app.MapHub<VisitorHub>("/VisitorHub"); //neyi t�ketece�imizi belirtiyoruz.
app.Run();
