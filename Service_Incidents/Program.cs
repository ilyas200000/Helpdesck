using Microsoft.EntityFrameworkCore;
using Service_Incidents.Data;

namespace Service_Incidents
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Récupère la chaîne de connexion à la base dans les paramètres
            string? connect = builder.Configuration.GetConnectionString("IncidentConnect");

            // Add services to the container.
            builder.Services.AddDbContext<IncidentsDbContext>(opt => opt.UseSqlServer(connect));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Enregistre les contrôleurs
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
