using WebApi.Interface;
using WebApi.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Añadir configuración desde appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Añadir servicios
builder.Services.AddScoped<IEstudianteService, EstudianteService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
