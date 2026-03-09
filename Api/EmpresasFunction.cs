using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using AppFE.Shared;

namespace Api
{
    public class EmpresasFunction
    {
            private readonly ApplicationDbContext _context;

            public EmpresasFunction(ApplicationDbContext context)
            {
                _context = context;
            }

            private record EmpResponse(List<Empresa> Empresas, List<string> Logs);

            [Function("GetEmpresas")]
            public async Task<HttpResponseData> Run(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "empresas")] HttpRequestData req)
            {
                var logs = new List<string>();
                List<Empresa> listado = null;

                try
                {
                    logs.Add("Iniciando consulta a la base de datos");
                    // Forzamos apertura de la conexión para capturar info
                    await _context.Database.OpenConnectionAsync();
                    var conn = _context.Database.GetDbConnection();
                    logs.Add($"Cadena de conexión usada: {conn.ConnectionString}");
                    logs.Add($"Proveedor: {conn.GetType().Name}");

                    listado = await _context.empresas.ToListAsync();
                    logs.Add($"Recuperados {listado.Count} registros.");
                }
                catch (Exception ex)
                {
                    logs.Add($"Error en base de datos: {ex.Message}");
                    // guardar lista vacía para no devolver null
                    listado = new List<Empresa>();
                }
                finally
                {
                    await _context.Database.CloseConnectionAsync();
                }

                // Crear respuesta
                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(new EmpResponse(listado, logs));
                return response;
            }
    }

}
