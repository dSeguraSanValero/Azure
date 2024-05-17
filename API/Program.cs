using Microsoft.EntityFrameworkCore;
using FisioSolution.Business;
using FisioSolution.Data;
using FisioSolution.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IPhysioRepository, PhysioRepository>();
builder.Services.AddScoped<IPhysioService, PhysioService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();

var connectionString = builder.Configuration.GetConnectionString("ServerDB_azure");

builder.Services.AddDbContext<MigrationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (connectionString == "ServerDB_azure") 
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<MigrationDbContext>();
        context.Database.Migrate();
    }
}

app.UseCors();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
