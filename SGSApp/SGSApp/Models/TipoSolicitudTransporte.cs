namespace SGSApp.Models
{
    public class TipoSolicitudTransporte
    {
        public long IdTipoSolicitudTransporte { get; set; }
        public string NombreTipo { get; set; }
        public bool? Temporal { get; set; }
        public bool? Permanente { get; set; }
        public string CampoFecha { get; set; }
        public bool? FechaMultipleHabilitada { get; set; }
        public string CampoHora { get; set; }
        public bool? CampoHoraHabilitado { get; set; }
        public string CampoDestino { get; set; }
        public bool? CampoDireccionHabilitado { get; set; }
        public string BotonDireccion { get; set; }
        public bool? BotonDireccionHabilitado { get; set; }
        public bool? CampoCompaneroHabilitado { get; set; }
        public string CampoPersonaAutorizada { get; set; }
        public bool? CampoNombrePHabilitado { get; set; }
        public bool? CampoIdentPHabilitado { get; set; }
        public string CampoTelefono { get; set; }
        public bool? CampoTelefonoHabilitado { get; set; }
        public bool? JornadaMananaHabilitada { get; set; }
        public bool? JornadaTardeHabilitada { get; set; }
        public bool? JornadaExtraHabilitada { get; set; }
        public bool? JornadaExtenHabilitada { get; set; }
        public string CampoObservaciones { get; set; }
        public bool? CampoObsHabilitado { get; set; }
        public string TipoPersonaHabilitada { get; set; }
        public string TipoGestion { get; set; }
        public bool? RequiereAutorizacion { get; set; }
        public bool? Automatico { get; set; }
        public string Accion { get; set; }
        public long? CanceladoPor { get; set; }
        public int? CreacionCorreo { get; set; }
        public int? AprobacionCorreo { get; set; }
        public int? RechazoCorreo { get; set; }
        public string DescripcionSolicitud { get; set; }
    }
}