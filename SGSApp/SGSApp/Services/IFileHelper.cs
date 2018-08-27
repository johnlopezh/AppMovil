namespace SGSApp.Services
{
    public interface IFileHelper
    {
        //Inteface que permite obtener para cada plataforma la ruta de la DB
        string GetLocalFilePath(string fileName);
    }
}