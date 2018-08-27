namespace SGSApp.Helper
{
    public static class GlobalVariables
    {
        public static string Usuario = "";
        public static string Token = "";
        public static string TokenSha = "";
        public static string TokenAcumen = "";
        public static byte[] imagenUsuario;
        public static string Email = "";
        public static string AcumenUrl = "http://naranjaweb.cloudapp.net/";

        public static void grabarUsuario(string username, string email)
        {
            Usuario = username;
            Email = email;
        }

        public static void grabarTokenOffice365(string tokenOffice365)
        {
            Token = tokenOffice365;
        }

        public static void grabarTokenSharepoint(string tokenSharepoint)
        {
            TokenSha = tokenSharepoint;
        }

        public static void grabarImagenUsuario(byte[] imagen)
        {
            imagenUsuario = imagen;
        }

        public static void grabarTokenAcumen(string tokenAcumen)
        {
            TokenAcumen = tokenAcumen;
        }
    }
}