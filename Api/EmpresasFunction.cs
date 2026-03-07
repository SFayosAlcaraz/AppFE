using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Http;
using AppFFE.Shared;

namespace Api;

public class EmpresasFunction
{

    public class EmpresasFunction
    {
        private readonly ApplicationDbContext _context;

        public EmpresasFunction(ApplicationDbContext context)
        {
            _context = context;
        }

        [Function("GetEmpresas")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "empresas")] HttpRequestData req)
        {
            // Consultar datos
            var listado = await _context.empresas.ToListAsync();

            // Crear respuesta
            var response = req.CreateResponse(HttpStatusCode.OK);
            
            // Escribir el JSON (esto requiere Microsoft.Azure.Functions.Worker.Http)
            await response.WriteAsJsonAsync(listado);

            return response;
        }
    }
}

