using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace SGSApp.Models
{
    public class NotificacionesPush : INotifyPropertyChanged
    {
        private string _fecha;
        private string _textoNotificacionPush;


        [PrimaryKey] [AutoIncrement] public int ID { get; set; }

        public string TextoNotificacionPush

        {
            get => _textoNotificacionPush;
            set
            {
                _textoNotificacionPush = value;
                OnPropertyChanged();
            }
        }

        public string Fecha
        {
            get => _fecha;
            set
            {
                _fecha = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}