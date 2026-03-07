using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Api.Data;

namespace Api
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
