using Microsoft.EntityFrameworkCore;
using SignalRApi.DAL;
using SignalRApi.Hubs;
using SignalRApi.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/// Add PostgreSQL Configure.
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<Context>(opt =>
opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<VisitorService>();
builder.Services.AddSignalR();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        //dýþarýdan herhangi bir kaynaðýn benim serverimi yani api projemi consume etmesine yani tüketmesine olanak saðlayan kýsým burasý.
         builder.AllowAnyHeader() //dýþarýdan herhangi bir baþlýðýn gelmesine izin ver.
         .AllowAnyMethod() //dýþarýdan herhangi bir metodun gelmesine izin ver.
         .SetIsOriginAllowed((host) => true)
         .AllowCredentials();
    }));




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapHub<VisitorHub>("/VisitorHub"); //neyi tüketeceðimizi belirtiyoruz.

app.Run();
