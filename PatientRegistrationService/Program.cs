using Microsoft.Extensions.Logging;
using PatientRegistrationService.Interfaces;
using PatientRegistrationService.Middleware;
using PatientRegistrationService.Services;
using Serilog;

namespace PatientRegistrationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // File Logger
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/myRegistrationService.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            //builder.Services.AddEndpointsApiExplorer();

            //builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IPatientService, PatientService>();

            builder.Services.AddSingleton<PatientDB>();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{

            //    app.UseSwagger();

            //    app.UseSwaggerUI();

            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            Log.CloseAndFlush();
        }
    }
}
