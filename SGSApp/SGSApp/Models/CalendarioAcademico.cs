using System;

namespace SGSApp.Models
{
    public class CalendarioAcademico
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaCalendario { get; set; }
        public string NombreDia { get; set; }
        public int? NumeroDia { get; set; }
        public int? NumeroCiclo { get; set; }
        public string TipoDia { get; set; }
        public string TipoHorarioPreescolar { get; set; }
        public string TipoHorarioPrimaria { get; set; }
        public string TipoHorarioBachillerato { get; set; }
        public string UsuarioLog { get; set; }
    }
}