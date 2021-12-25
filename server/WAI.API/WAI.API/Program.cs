using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WAI.API.DataAccess;
using WAI.API.DataAccess.Entities;
using WAI.API.Hubs;
using WAI.API.Services;

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

var services = builder.Services;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("WAI.Db")));

var idbuilder = services.AddIdentityCore<User>();
var identityBuilder = new IdentityBuilder(idbuilder.UserType, services);
identityBuilder.AddEntityFrameworkStores<DataContext>();
identityBuilder.AddSignInManager<SignInManager<User>>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateAudience = false,
                ValidateIssuer = false
            };

            opt.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["Bearer"];
                    
                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/game")))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                }
            };
        });


builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddSingleton<IVkClientFactory, VkClientFactory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<GameHub>("/game");

app.Run();