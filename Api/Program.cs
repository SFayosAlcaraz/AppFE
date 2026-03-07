using Microsoft.Extensions.Hosting; // <--- ESTE ES EL IMPORTANTE
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Functions.Worker;
using AppFE.Api.Data;
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

host.Run();