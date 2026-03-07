using Microsoft.Extensions.Hosting; // <--- ESTE ES EL IMPORTANTE
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Functions.Worker;
using Api.Data;
using AppFE.Shared;
using System;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString") 
            ?? throw new InvalidOperationException("Falta la cadena de conexión SqlConnectionString");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    
    if (!context.empresas.Any())
    {
        context.empresas.AddRange(
            new Empresa { cif = "A12345678", nombre = "Empresa 1", localidad = "Madrid", plazas_ofertadas = 10 },
            new Empresa { cif = "B98765432", nombre = "Empresa 2", localidad = "Barcelona", plazas_ofertadas = 5 }
        );
        context.SaveChanges();
    }
}

host.Run();