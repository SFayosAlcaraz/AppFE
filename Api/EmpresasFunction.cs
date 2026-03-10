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

            [Function("GetEmpresas")]
            public async Task<HttpResponseData> Run(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "empresas")] HttpRequestData req)
            {
                // Consultar datos
                var listado = await _context.empresas.ToListAsync();

                // Crear respuesta
                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(listado);

                return response;
            }

            [Function("CreateEmpresa")]
            public async Task<HttpResponseData> Create(
                [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "empresas")] HttpRequestData req)
            {
                var empresa = await req.ReadFromJsonAsync<Empresa>();
                if (empresa == null)
                {
                    var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                    await bad.WriteStringAsync("Request body was not a valid Empresa");
                    return bad;
                }

                // La base de datos generará el id por Identity
                _context.empresas.Add(empresa);
                await _context.SaveChangesAsync();

                var response = req.CreateResponse(HttpStatusCode.Created);
                await response.WriteAsJsonAsync(empresa);
                return response;
            }

            [Function("UpdateEmpresa")]
            public async Task<HttpResponseData> Update(
                [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "empresas/{id}")] HttpRequestData req,
                int id)
            {
                var existing = await _context.empresas.FindAsync(id);
                if (existing == null)
                {
                    var nf = req.CreateResponse(HttpStatusCode.NotFound);
                    await nf.WriteStringAsync("Empresa no encontrada");
                    return nf;
                }

                var updated = await req.ReadFromJsonAsync<Empresa>();
                if (updated == null)
                {
                    var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                    await bad.WriteStringAsync("Request body was not a valid Empresa");
                    return bad;
                }

                // Copiar propiedades (except id)
                existing.cif = updated.cif;
                existing.nombre = updated.nombre;
                existing.sector = updated.sector;
                existing.direccion = updated.direccion;
                existing.localidad = updated.localidad;
                existing.codigo_postal = updated.codigo_postal;
                existing.tutor_empresa = updated.tutor_empresa;
                existing.telefono_contacto = updated.telefono_contacto;
                existing.email_contacto = updated.email_contacto;
                existing.plazas_ofertadas = updated.plazas_ofertadas;
                existing.convenio_activo = updated.convenio_activo;
                existing.fecha_registro = updated.fecha_registro;

                await _context.SaveChangesAsync();

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(existing);
                return response;
            }
    }

}
