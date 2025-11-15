using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ColetaAPI.Service.ColetaService;
using ColetaAPI.Service.LocalizacaoService;
using ColetaAPI.Service.DatabaseService;
using ColetaAPI.DataContext;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Ignora referências circulares
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        // Opcional: escreve JSON indentado (mais legível)
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICollectionInterface, CollectionService>();
builder.Services.AddScoped<ILocationInterface, LocationService>();
builder.Services.AddScoped<IDatabaseInterface, DatabaseService>();

// Conexao com o SQL Server.
builder.Services.AddDbContext<ApplicationsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ColetaAPI");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
