using Microsoft.EntityFrameworkCore;
using Rinha2024.Data.Context;
using Rinha2024.Data.Repositories;
using Rinha2024.Data.Repositories.Interface;
using System;


if (args.Contains("--RunMigrations"))
{
    // Need to use ConfigurationBuilder to retrieve connection string from appsettings.json


    var optionsBuilder = new DbContextOptionsBuilder<RinhaContext>();
    //string db_connection = @"User ID=postgres;
    //                                Password=123456;
    //                                Host=postgres;
    //                                Port=5432;
    //                                Database=Rinha;
    //                                Pooling=true;                                                       Connection Lifetime=0;
    //                                Include Error Detail=true";

    string db_connection = Environment.GetEnvironmentVariable("DB_CONNECTION");

    Console.WriteLine(db_connection);
    optionsBuilder.UseNpgsql(db_connection);

    // There is no DI at current step, so need to create context manually
    await using var dbContext = new RinhaContext(optionsBuilder.Options);
    Console.WriteLine("1");
    await dbContext.Database.MigrateAsync();
    Console.WriteLine("2");

}

Console.WriteLine("teste");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RinhaContext>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<ITransacaoRepository, TransacaoRepository>();


var port = builder.Configuration["API_PORT"];
builder.WebHost.UseUrls($"http://*:{port}");


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
Console.WriteLine("3");
app.Run();
