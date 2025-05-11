using System.Data;
using CityzenHackathon2025.API.Hubs;
using CitizenHackathon2025.BLL.Interfaces;
using CitizenHackathon2025.BLL.Services;
using CitizenHackathon2025.DAL.Interfaces;
using CitizenHackathon2025.DAL.Repositories;
using Microsoft.Data.SqlClient;
using CitizenHackathon2025.DAL.Entities;
using CityzenHackathon2025.API.Tools;
using CitizenHackathon2025.API.Hubs;
using CityzenHackathon2025.API;

var builder = WebApplication.CreateBuilder(args);
#nullable disable
// Add services to the container.

// SQLConnection

builder.Services.AddScoped<IDbConnection>(static sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("default");
    return new SqlConnection(connectionString);
});

// Injections

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IPlaceService, PlaceService>();
builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();
builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();
builder.Services.AddScoped<ITrafficConditionService, TrafficConditionService>();
builder.Services.AddScoped<ITrafficConditionRepository, TrafficConditionRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserHubService, UserHubService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddScoped<IWeatherHubService, WeatherHubService>();
builder.Services.AddSingleton<IHostedService, WeatherService>();
builder.Services.AddHttpClient<IOpenWeatherMapService, OpenWeatherMapService>();
builder.Services.AddScoped<ICrowdInfoRepository, CrowdInfoRepository>();

builder.Services.AddHttpClient<ChatGptService>();
builder.Services.AddHttpClient<IAIService, AIService>();
builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAI"));
builder.Logging.AddConsole();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
    });



// SignalR
builder.Services.AddSignalR();
builder.Services.AddHostedService<WeatherService>();

// Add Hubs

builder.Services.AddSingleton<CrowdHub>();
builder.Services.AddSingleton<EventHub>();
builder.Services.AddSingleton<GPTHub>();
builder.Services.AddSingleton<PlaceHub>();
builder.Services.AddSingleton<SuggestionHub>();
builder.Services.AddSingleton<TrafficHub>();
builder.Services.AddSingleton<UpdateHub>();
builder.Services.AddSingleton<UserHub>();
builder.Services.AddSingleton<WeatherForecastHub>();

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

// Token Generator

builder.Services.AddScoped<TokenGenerator>();

// Security levels
// Declaration of the different security levels to be implemented in the controller using the attribute [Authorize("font_name")]
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
    o.AddPolicy("Modo", policy => policy.RequireClaim("role", "admin", "modo"));
    o.AddPolicy("User", policy => policy.RequireClaim("role", "user"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CitizenHackathon2025 API V1");
        c.RoutePrefix = "swagger";
    });
}
else
{
    // Swagger désactivé en production par sécurité
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Commun
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAnyOrigin");
app.UseAuthorization();

app.UseEndpoints(Endpoints =>
{
    Endpoints.MapControllers();

    Endpoints.MapHub<EventHub>("/hubs/eventHub");
    Endpoints.MapHub<PlaceHub>("/hubs/placeHub");
    Endpoints.MapHub<SuggestionHub>("/hubs/suggestionHub");
    Endpoints.MapHub<TrafficHub>("/hubs/trafficHub");
    Endpoints.MapHub<UpdateHub>("/hubs/updateHub");
    Endpoints.MapHub<UserHub>("/hubs/userHub");
    Endpoints.MapHub<CrowdHub>("/hubs/crowdHub");
    Endpoints.MapHub<WeatherForecastHub>("/hubs/weatherforecastHub");
});

app.MapGet("/api/weatherforecast", () =>
{
    var rng = new Random();
    return new WeatherForecast
    {
        DateWeather = DateTime.Now,
        TemperatureC = "rng.Next(-20, 55)",
        Summary = "Static",
        RainfallMm = "rng.Next(0, 100)",
        Humidity = "rng.Next(30, 100)",
        WindSpeedKmh = "rng.Next(0, 200) * 100"
    }; 
});

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    await next();
});
app.MapControllers();

app.Run();
