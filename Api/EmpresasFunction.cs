using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public class EmpresasFunction
{
    private readonly ILogger<EmpresasFunction> _logger;

    public EmpresasFunction(ILogger<EmpresasFunction> logger)
    {
        _logger = logger;
    }

    [Function("EmpresasFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}
