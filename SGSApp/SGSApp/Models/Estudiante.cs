using System.ComponentModel;
using Xamarin.Forms;

namespace SGSApp.Models
{
    public class Estudiante : INotifyPropertyChanged
    {
        public string UrlFoto { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoEstudiante { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string IdFamilia { get; set; }
        public string TipoPaciente { get; set; }
        public ImageSource AddImage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}