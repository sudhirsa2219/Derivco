using Derivco.Core.DataService;
using Derivco.Core.DataService.EFCore;
using Derivco.Core.DomainService;
using Derivco.DataServices.EFCore;
using Derivco.DataServices.EFCore.DataContext;
using Derivco.DataServices.Interfaces;
using Derivco.DomainServices;
using Derivco.EFCore.Setup;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Derivco.WebApi",
        Version = "v1.0.0",
        Description = "Derivco Simple Web Api for game of roulette",
        Contact = new OpenApiContact
        {
            Name = "Derivco Website",
            Email = string.Empty,
            Url = new Uri("https://www.Derivco.com")
        }
    });
});


builder.Services.AddScoped(typeof(IEntityDataService<>), typeof(EntityDataService<>));
builder.Services.AddScoped(typeof(DomainService<,>));

builder.Services.AddScoped<AppDbContext, InMemoryDbContext>();
builder.Services.AddScoped<IEmployeeDataService, EmployeeDataService>();
builder.Services.AddScoped<EmployeeDomainService>();

DbContextDataInitializer.Initialize(new InMemoryDbContext());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
