using GolCheckApi.Models.DbSettings;
using GolCheckApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PrePlayerDatabaseSettings>(
    builder.Configuration.GetSection("PremierPlayerDatabase"));

builder.Services.Configure<FrLigueDatabaseSettings>(
    builder.Configuration.GetSection("FrLigueDatabase"));

builder.Services.Configure<SerieADatabaseSettings>(
    builder.Configuration.GetSection("ÝtalyaSerieADatabase"));

builder.Services.Configure<BundesligaDatabaseSettings>(
    builder.Configuration.GetSection("AlmanyaBundesligaDatabase"));

builder.Services.Configure<PremierLigDatabaseSettings>(
    builder.Configuration.GetSection("ÝngilterePremierLigDatabase"));

builder.Services.Configure<IspanyaDatabaseSettings>(
    builder.Configuration.GetSection("ÝspanyaLaLigaDatabase"));

builder.Services.Configure<LaligaPlDatabaseSettings>(
    builder.Configuration.GetSection("ÝspanyaLaLigaOyuncuDatabase"));

builder.Services.AddSingleton<FrLigueService>();
builder.Services.AddSingleton<PrePlayerService>();
builder.Services.AddSingleton<SerieAService>();
builder.Services.AddSingleton<BundesligaService>();
builder.Services.AddSingleton<PreLigService>();
builder.Services.AddSingleton<IspanyaService>();
builder.Services.AddSingleton<LaligaPlayerService>();


// Add services to the container.

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
