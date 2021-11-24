using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
Log.Information("Logger Initialized - from now, everything will be logged using Serilog");


try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("InMemoryTest"));

    builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
    var app = builder.Build();
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    SeedDb.PrepPopulation(app);
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal("A fatal Error occured!\n");
    Log.Fatal(ex.Message);
    Console.WriteLine(ex.Message);
}
finally
{
    Log.Information("Close Logger and flush");
    Log.CloseAndFlush();
}
