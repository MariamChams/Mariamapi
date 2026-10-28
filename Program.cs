
using MariamPortfolio.Data;
using MariamPortfolio.Services;
using Microsoft.EntityFrameworkCore;

namespace MariamPortfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers gebruiken
            builder.Services.AddControllers();

            // Swagger toevoegen
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Dependency injection voor services
            builder.Services.AddScoped<ProjectService>();
            builder.Services.AddScoped<BlogpostService>();

            // Database registreren met EF Core
            builder.Services.AddDbContext<PortfolioDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("PortfolioDatabase")));

            var app = builder.Build();



            // Swagger pagina aanzetten
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Controllers koppelen
            app.MapControllers();

            app.Run();
        }
    }
}
