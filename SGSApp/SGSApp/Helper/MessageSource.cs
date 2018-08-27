namespace SGSApp.Helper
{
    public class MessageSource
    {
        /*Descripción de los  botones*/

        #region Botones

        public static string buttonTextOk { get; set; } = "Ok";

        #endregion


        /*Mensajes de Descripción*/

        #region MsgDescripcion

        public static string messageNoticias { get; set; } =
            "Ha ocurrido un error al cargar la sección de noticias. Por favor comprueba tu conexión a Internet e inténtalo de nuevo.";

        public static string messageIniciarSesion { get; set; } =
            "Error al iniciar sesión. Por favor comprueba tu conexión a Internet e inténtalo de nuevo.";

        public static string messageDashboardConsulta { get; set; } =
            "Ha ocurrido un error al cargar el dashboard. Por favor comprueba tu conexión a Internet e inténtalo de nuevo.";

        public static string messageCalendario { get; set; } =
            "Ha ocurrido un error al cargar el calendario. Por favor comprueba tu conexión a Internet e inténtalo de nuevo.";

        public static string messageExtensiones { get; set; } =
            "Ha ocurrido un error al cargar las extensiones. Por favor comprueba tu conexión a Internet e inténtalo de nuevo.";

        public static string messageDireccion { get; set; } =
            "Ha ocurrido un error al guardar la dirección. Por favor comprueba tu conexión a Internet e inténtalo de nuevo.";

        #endregion

        /*Mensajes de Titulo*/

        #region Titulo  

        public static string titleGeneral { get; set; } = "¡Lo sentimos!";

        public static string titleInicarSesion { get; set; } = "¡Error al iniciar sesión!";

        #endregion
    }
}