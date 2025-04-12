using System.Data.SqlClient;
using CityzenHackathon2025.API.Hubs;
using CitizenHackathon2025.BLL.Interfaces;
using CitizenHackathon2025.BLL.Services;
using CitizenHackathon2025.DAL.Interfaces;
using CitizenHackathon2025.DAL.Repositories;
using Microsoft.Data.SqlClient;
using CitizenHackathon2025.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);
#nullable disable
// Add services to the container.

// SQLConnection

builder.Services.AddScoped<SqlConnection>(Sc => new SqlConnection(builder.Configuration.GetConnectionString("default")));

// Injections

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IPlaceService, PlaceService>();
builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();
builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();
builder.Services.AddScoped<ITrafficConditionService, TrafficConditionService>();
builder.Services.AddScoped<ITrafficConditionRepository, TrafficConditionRepository>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.AddHttpClient<ChatGptService>();
builder.Services.AddHttpClient<IAIService, AIService>();
builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAI"));
builder.Logging.AddConsole();

builder.Services.AddControllers();

// SignalR
builder.Services.AddSignalR();

// Add Hubs

builder.Services.AddScoped<EventHub>();
builder.Services.AddScoped<PlaceHub>();
builder.Services.AddScoped<SuggestionHub>();
builder.Services.AddScoped<TrafficHub>();
builder.Services.AddScoped<WeatherForecastHub>();

// Connection

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
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
app.UseRouting();
app.UseCors("AllowAnyOrigin");
app.UseAuthorization();

app.UseEndpoints(Endpoints =>
{
    Endpoints.MapHub<EventHub>("/hubs/eventHub");
    Endpoints.MapHub<PlaceHub>("/hubs/placeHub");
    Endpoints.MapHub<SuggestionHub>("/hubs/suggestionHub");
    Endpoints.MapHub<TrafficHub>("/hubs/trafficHub");
    Endpoints.MapHub<WeatherForecastHub>("/hubs/weasterforecastHub");
});
app.MapGet("/api/weatherforecast", () =>
{
    var rng = new Random();
    return new WeatherForecast
    {
        DateWeather = DateTime.Now,
        TemperatureC = rng.Next(-20, 55),
        Summary = "Sunny",
        RainfallMm = rng.Next(0, 100),
        Humidity = rng.Next(0, 100),
        WindSpeedKmh = rng.Next(0, 200) * 100
    }; 
});

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    await next();
});
//app.MapControllers();

app.Run();
