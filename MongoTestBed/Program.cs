using MongoTestBed.Models;
using MongoTestBed.Services;
using MongoTestBed.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;

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

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
            });
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

    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && reader.TryGetDateTimeOffset(out DateTimeOffset dateTimeOffset))
            {
                return dateTimeOffset;
            }

            throw new JsonException("Invalid DateTimeOffset value");
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'"));
        }
    }
}