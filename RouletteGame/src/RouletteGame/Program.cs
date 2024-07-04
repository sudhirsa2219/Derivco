using RouletteGame;
using RouletteGame.Infrastructure;
using RouletteGame.Services;
using System.Reflection;


var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args);

IConfiguration configuration = config.Build();

var builder = WebApplication.CreateBuilder(args);


var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new DapperContext(connectionString))
    .AddScoped<BetRepository>()
    .AddScoped<SpinRepository>()
    .AddSingleton<RouletteService>()
    .AddMediatR(config => {
        config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    })
    .AddApiServices(builder.Configuration)
    .AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Roulette Game Web Api", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiServices();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
