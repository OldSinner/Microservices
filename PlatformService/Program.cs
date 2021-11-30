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

    //Services Section
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    // Database Contexts
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("InMemoryTest"));
    //Custom Services
    builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

    // App setting Section
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseSerilogRequestLogging();

    // app.UseHttpsRedirection();

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



