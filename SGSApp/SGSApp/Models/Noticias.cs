using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace SGSApp.Models
{
    public class Noticias : INotifyPropertyChanged
    {
        public string TituloNoticia { get; set; }
        public string Resumen { get; set; }
        public string Descripcion { get; set; }

        public ImageSource ImageURL { get; set; }

        //public  ImageSource ImageSource { get { return ImageSource.FromUri(new Uri(ImageURL)); } set { } }

        public Type TargetType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}