using Application.Behaviour;
using Application.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using Persistence;
using Persistence.Repositories;

namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationContext>();

            builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOwnerRequestRepository, OwnerRequestRepository>();
            builder.Services.AddScoped<ISpamReportRepository, SpamReportRepository>();
            builder.Services.AddScoped<IFavouriteInstitutionRepository, FavouriteInstitutionRepository>();
            builder.Services.AddScoped<IVisitingRepository, VisitingRepository>();
            builder.Services.AddScoped<IReviewScoreRepository, ReviewScoreRepository>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Application.Behaviour.Review.CreateReviewCommand).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            builder.Services.AddValidatorsFromAssembly(
                typeof(Application.Behaviour.Review.CreateReviewCommand).Assembly);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}