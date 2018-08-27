using System;
using System.ComponentModel;

namespace SGSApp.Models
{
    public class CalendarioE : INotifyPropertyChanged
    {
        public string tituloEvento { get; set; }
        public DateTime diaEvento { get; set; }
        public string descripcion { get; set; }
        public string duracion { get; set; } = "Todo el día: ";
        public string grupoEvento { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}