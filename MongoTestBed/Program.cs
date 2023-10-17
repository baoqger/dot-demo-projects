using MongoTestBed.Models;
using MongoTestBed.Services;
using MongoTestBed.Repositories;

namespace MongoTestBed
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<OrderStoreDatabaseSettings>(builder.Configuration.GetSection("OrderStoreDatabase"));

            builder.Services.Configure<AlarmStoreDatabaseSettings>(builder.Configuration.GetSection("AlarmStoreDatabase"));

            builder.Services.AddSingleton<OrdersService>();
            builder.Services.AddSingleton<AlarmsService>();
            builder.Services.AddSingleton<AlarmsRepository>();

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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}