using Application.Behaviour;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using FluentValidation;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Repositories;
using System.Text;

namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                                { Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddDbContext<ApplicationContext>();

            builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOwnerRequestRepository, OwnerRequestRepository>();
            builder.Services.AddScoped<ISpamReportRepository, SpamReportRepository>();
            builder.Services.AddScoped<IFavouriteInstitutionRepository, FavouriteInstitutionRepository>();
            builder.Services.AddScoped<IVisitingRepository, VisitingRepository>();
            builder.Services.AddScoped<IReviewScoreRepository, ReviewScoreRepository>();
            builder.Services.Configure<MinioOptions>(builder.Configuration.GetSection(MinioOptions.Section));
            builder.Services.AddScoped<IFileService, FileService>();

            builder.Services.AddHttpClient<IVkAuthService, VkAuthService>();
            builder.Services.AddSingleton<IJwtService, JwtService>();

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Section));
            builder.Services.Configure<VkOptions>(builder.Configuration.GetSection(VkOptions.Section));

            string jwtSecret = builder.Configuration["JwtOptions:Secret"]!;
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
                        ValidAudience = builder.Configuration["JwtOptions:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSecret))
                    };
                });

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(Application.Behaviour.Review.CreateReviewCommand).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            builder.Services.AddValidatorsFromAssembly(typeof(Application.Behaviour.Review.CreateReviewCommand).Assembly);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}