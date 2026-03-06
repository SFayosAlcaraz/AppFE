using System;

namespace TuProyecto.Shared
{
    public class Empresa
    {
        public int id { get; set; }
        public string cif { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string? sector { get; set; }
        public string? direccion { get; set; }
        public string? localidad { get; set; }
        public string? codigo_postal { get; set; }
        public string? tutor_empresa { get; set; }
        public string? telefono_contacto { get; set; }
        public string? email_contacto { get; set; }
        public int? plazas_ofertadas { get; set; }
        public bool? convenio_activo { get; set; }
        public DateTime? fecha_registro { get; set; }
    }
}