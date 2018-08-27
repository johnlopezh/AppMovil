namespace SGSApp.Models
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        public int IdGrupoFamiliar { get; set; }
        public int IdDepartamento { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion1 { get; set; }
        public string Barrio { get; set; }
        public string TelefonoDireccion { get; set; }
        public bool DireccionPrincipal { get; set; }
        public string UsuarioLog { get; set; }
        public string Sector { get; set; }
        public long? Latitud { get; set; }
        public long? Longitud { get; set; }
        public string Zona { get; set; }
        public string PuntoReferente { get; set; }
        public string RestriccionBus { get; set; }
        public string Estrato { get; set; }
        public string DescripcionDireccion { get; set; }

        /// <summary>
        ///     Nonmbre de la ciudad en donde se encuentra la dirección.
        /// </summary>
        public string NombreCiudad { get; set; }

        /// <summary>
        ///     Dirección asignada al grupo familiar.
        /// </summary>
        public string DireccionGrupo { get; set; }

        /// <summary>
        ///     Ciudad empleado al grupo familiar.
        /// </summary>
        public string CiudadEmpleado { get; set; }

        /// <summary>
        ///     Ciudad empleado al grupo familiar.
        /// </summary>
        public string DireccionEmpleado { get; set; }

        /// <summary>
        ///     Ciudad empleado al grupo familiar.
        /// </summary>
        public string BarrioEmpleado { get; set; }

        /// Ciudad empleado al grupo familiar.
        /// </summary>
        public string EstratoEmpleado { get; set; }

        /// Ciudad empleado al grupo familiar.
        /// </summary>
        public string TelefonoEmpleado { get; set; }

        /// Ciudad empleado al grupo familiar.
        /// </summary>
        public string DirDescripcionEmpleado { get; set; }

        /// Ciudad empleado al grupo familiar.
        /// </summary>
        public bool DirPrincipalEmpleado { get; set; }
    }
}