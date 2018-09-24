using System;

namespace SGSApp.Models
{
    public class SolicitudTransporteApp
    {
        public long? IdSolicitudTransporte { get; set; }
        public long? IdTipoSolicitudTransporte { get; set; }
        public string TipoIdentSolicitante { get; set; }
        public string IdentificacionSolicitante { get; set; }
        public bool? Temporal { get; set; }
        public bool? Permanente { get; set; }
        public TimeSpan? Hora { get; set; }
        public Int64? IdDireccion { get; set; }
        public string IdentificacionCompanero { get; set; }
        public string TipoIdentCompanero { get; set; }
        public string NombreAutorizado { get; set; }
        public string IdentificacionAutorizado { get; set; }
        public Int64? Telefono { get; set; }
        public bool? JornadaMananaHabilitada { get; set; }
        public bool? JornadaTardeHabilitada { get; set; }
        public bool? JornadaExtraHabilitada { get; set; }
        public bool? JornadaExtenHabilitada { get; set; }
        public string Observaciones { get; set; }
        public string EstadoSolicitud { get; set; }
        public string UsuarioLog { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public int? PeriodoLectivo { get; set; }
        public int? MotivoRechazo { get; set; }
        public string fechas { get; set; }
        public int? rol { get; set; }
    }
}