using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ColetaAPI.Service.ColetaService;
using ColetaAPI.Service.LocalizacaoService;
using ColetaAPI.DataContext;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IColetaInterface, ColetaService>();
builder.Services.AddScoped<ILocalizacaoInterface, LocalizacaoService>();

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
