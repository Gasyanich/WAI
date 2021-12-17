using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WAI.API.DataAccess;
using WAI.API.DataAccess.Entities;
using WAI.API.Hubs;
using WAI.API.Services;
using WAI.API.Services.Games;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy",
        b => b.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("WAI.Db")));
builder.Services.AddIdentity<User, IdentityRole<long>>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDataProtection();
builder.Services.AddHttpClient();

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddSingleton<IVkClientFactory, VkClientFactory>();
builder.Services.AddScoped<IGamesService, GamesService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapHub<GameHub>("/game");
app.MapHub<GamesHub>("/games");

app.Run();