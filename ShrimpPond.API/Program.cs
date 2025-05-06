using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using ShrimpPond.API.Authorization.Data;
using ShrimpPond.API.Hubs;
using ShrimpPond.API.Middleware;
using ShrimpPond.API.Woker;
using ShrimpPond.Application;
using ShrimpPond.Infrastructure;
using ShrimpPond.Infrastructure.Communication;
using ShrimpPond.Persistence;
using System.Text;

namespace ShrimpPond.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();

            builder.Services.Configure<MqttOptions>(builder.Configuration.GetSection("MqttOptions"));
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<ManagedMqttClient>();
            builder.Services.AddHostedService<HostMachineWorker>();

            // For Entity Framework
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ShrimpPondAuth")));

            // For Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                    ClockSkew = TimeSpan.Zero // Không cho phép dung sai thời gian
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                           builder =>
                           {
                               builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                           });
            });

            builder.Services.AddMemoryCache();

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.MapControllers();
            app.MapHub<NotificationHub>("/machineHub");

            app.Run();

        }
    }
}

